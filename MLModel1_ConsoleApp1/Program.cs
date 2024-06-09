using System;
using Microsoft.ML;
using System.IO;
using System.Linq;
using Microsoft.ML.Data;
using System.Drawing;
using System.Diagnostics;

class Program
{
    private static string ModelPath = ("");

    private static string TrainingDataLocation = ("");

    static void Main(string[] args)
    {
        Color color = Color.FromArgb(130, 150, 115);

        //Call the following piece of code for splitting the ratings.csv into ratings_train.csv and ratings.test.csv.
        // Program.DataPrep();

        //STEP 1: Create MLContext to be shared across the model creation workflow objects
        MLContext mlContext = new MLContext();

        //STEP 2: Read data from text file using TextLoader by defining the schema for reading the movie recommendation datasets and return dataview.
        var trainingDataView = mlContext.Data.LoadFromTextFile<ModelInput>(path: TrainingDataLocation, hasHeader: true, separatorChar: ',');

        Console.WriteLine("=============== Reading Input Files ===============", color);
        Console.WriteLine();

        // ML.NET doesn't cache data set by default. Therefore, if one reads a data set from a file and accesses it many times, it can be slow due to
        // expensive featurization and disk operations. When the considered data can fit into memory, a solution is to cache the data in memory. Caching is especially
        // helpful when working with iterative algorithms which needs many data passes. Since SDCA is the case, we cache. Inserting a
        // cache step in a pipeline is also possible, please see the construction of pipeline below.
        trainingDataView = mlContext.Data.Cache(trainingDataView);

        Console.WriteLine("=============== Transform Data And Preview ===============", color);
        Console.WriteLine();

        //STEP 4: Transform your data by encoding the two features userId and movieID.
        //        These encoded features will be provided as input to FieldAwareFactorizationMachine learner
        var dataProcessPipeline = mlContext.Transforms.Text.FeaturizeText(outputColumnName: "userIdFeaturized", inputColumnName: nameof(ModelInput.UserId))
                                      .Append(mlContext.Transforms.Text.FeaturizeText(outputColumnName: "movieIdFeaturized", inputColumnName: nameof(ModelInput.ProductId))
                                      .Append(mlContext.Transforms.Concatenate("Features", "userIdFeaturized", "movieIdFeaturized")));

        // STEP 5: Train the model fitting to the DataSet
        Console.WriteLine("=============== Training the model ===============", color);
        Console.WriteLine();
        var trainingPipeLine = dataProcessPipeline.Append(mlContext.BinaryClassification.Trainers.FieldAwareFactorizationMachine(new string[] { "Features" }));
        var model = trainingPipeLine.Fit(trainingDataView);

        //STEP 6: Evaluate the model performance

        //STEP 7:  Try/test a single prediction by predicting a single movie rating for a specific user
        Console.WriteLine("=============== Test a single prediction ===============", color);
        Console.WriteLine();
        var predictionEngine = mlContext.Model.CreatePredictionEngine<ModelInput, MovieRatingPrediction>(model);
        ModelInput testData = new ModelInput() { UserId = "6", ProductId = "10" };

        var movieRatingPrediction = predictionEngine.Predict(testData);
        Console.WriteLine($"UserId:{testData.UserId} with movieId: {testData.ProductId} Score:{Sigmoid(movieRatingPrediction.Score)} and Label {movieRatingPrediction.PredictedLabel}", Color.YellowGreen);
        Console.WriteLine();

        //STEP 8:  Save model to disk
        Console.WriteLine("=============== Writing model to the disk ===============", color);
        Console.WriteLine(); mlContext.Model.Save(model, trainingDataView.Schema, ModelPath);

        Console.WriteLine("=============== Re-Loading model from the disk ===============", color);
        Console.WriteLine();
        ITransformer trainedModel;
        using (FileStream stream = new FileStream(ModelPath, FileMode.Open, FileAccess.Read, FileShare.Read))
        {
            trainedModel = mlContext.Model.Load(stream, out var modelInputSchema);
        }

        Console.WriteLine("Press any key ...");
        Console.Read();
    }

    /*
     * FieldAwareFactorizationMachine the learner used in this example requires the problem to setup as a binary classification problem.
     * The DataPrep method performs two tasks:
     * 1. It goes through all the ratings and replaces the ratings > 3 as 1, suggesting a movie is recommended and ratings < 3 as 0, suggesting
          a movie is not recommended
       2. This piece of code also splits the ratings.csv into rating-train.csv and ratings-test.csv used for model training and testing respectively.
     */
    public static void DataPrep()
    {

        string[] dataset = File.ReadAllLines(@".\Data\ratings.csv");

        string[] new_dataset = new string[dataset.Length];
        new_dataset[0] = dataset[0];
        for (int i = 1; i < dataset.Length; i++)
        {
            string line = dataset[i];
            string[] lineSplit = line.Split(',');
            double rating = Double.Parse(lineSplit[2]);
            rating = rating > 3 ? 1 : 0;
            lineSplit[2] = rating.ToString();
            string new_line = string.Join(',', lineSplit);
            new_dataset[i] = new_line;
        }
        dataset = new_dataset;
        int numLines = dataset.Length;
        var body = dataset.Skip(1);
        var sorted = body.Select(line => new { SortKey = Int32.Parse(line.Split(',')[3]), Line = line })
                         .OrderBy(x => x.SortKey)
                         .Select(x => x.Line);
        File.WriteAllLines(@"../../../Data\ratings_train.csv", dataset.Take(1).Concat(sorted.Take((int)(numLines * 0.9))));
        File.WriteAllLines(@"../../../Data\ratings_test.csv", dataset.Take(1).Concat(sorted.TakeLast((int)(numLines * 0.1))));
    }

    public static float Sigmoid(float x)
    {
        return (float)(100 / (1 + Math.Exp(-x)));
    }

    public static string GetAbsolutePath(string relativeDatasetPath)
    {
        FileInfo _dataRoot = new FileInfo(typeof(Program).Assembly.Location);
        string assemblyFolderPath = _dataRoot.Directory.FullName;

        string fullPath = Path.Combine(assemblyFolderPath, relativeDatasetPath);

        return fullPath;
    }
}


        public class ModelInput
        {
            [ColumnName(@"Id")]
            public string Id { get; set; }

            [ColumnName(@"ProductId")]
            public string ProductId { get; set; }

            [ColumnName(@"ProducerName")]
            public string ProducerName { get; set; }

            [ColumnName(@"Amount")]
            public float Amount { get; set; }

            [ColumnName(@"Price")]
            public float Price { get; set; }

            [ColumnName(@"UserId")]
            public string UserId { get; set; }

            [ColumnName(@"Bought")]
            public bool Bought { get; set; }

            [ColumnName(@"BrandTag")]
            public float BrandTag { get; set; }

        }

public class MovieRatingPrediction
{
    public bool PredictedLabel;

    public float Score;
}