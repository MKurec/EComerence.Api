<template>
  <v-card class="overflow-hidden" color="teal lighten-5">
    <v-toolbar flat color="teal lighten-2">
      <v-toolbar-title class="font-weight-light">
        Dodaj produkt {{ productid}}
      </v-toolbar-title>
      <v-spacer></v-spacer>
    </v-toolbar>
    <v-card-text>
      <v-text-field
        v-model="product.name"
        color="black"
        label="Nazwa"
      ></v-text-field>
      <v-text-field
        v-model="product.amount"
        color="black"
        label="Ilosc"
        type="number"
      ></v-text-field>
      <v-text-field
        v-model="product.price"
        color="black"
        label="Cena"
        type="number"
      ></v-text-field>
      <v-combobox
        v-model="product.producerName"
        :items="producers.map((a) => a.name)"
        label="Producent"
      ></v-combobox>
      <v-autocomplete
        v-model="product.categoryName"
        :items="categories.map((a) => a.name)"
        label="Kategoria"
        required
      ></v-autocomplete>
      <v-textarea
        v-model="product.description"
        auto-grow
        label="Opis przedmiotu"
        rows="1"
      ></v-textarea>
      <v-select
        v-model="product.brandTag"
        :items="brandTags"
        label="brandTag"
        required
      ></v-select>
      <v-file-input
        v-model="photo"
        label="Dodaj Zdjęcie"
        filled
        accept="image/png, image/jpeg, image/bmp"
        prepend-icon="mdi-camera"
      ></v-file-input>
    </v-card-text>
    <v-divider></v-divider>
    <v-card-actions>
      <v-spacer></v-spacer>
      <v-btn color="success" @click="addProduct"> Dodaj </v-btn>
    </v-card-actions>
    <v-snackbar v-model="hasSaved" :timeout="2000" absolute bottom left>
      Your profile has been updated
    </v-snackbar>
  </v-card>
</template>
<script>
export default {
  data() {
    return {
      hasSaved: false,
      model: null,
      brandTags: ["premium", "medium", "budget"],
      product: {
        name: "",
        amount: "",
        price: "",
        producerName: "",
        categoryName: "",
        description: "",
        brandTag: "",
      },

    };
  },

  methods: {
    customFilter(item, queryText, itemText) {
      const textOne = item.name.toLowerCase();
      const textTwo = item.abbr.toLowerCase();
      const searchText = queryText.toLowerCase();

      return (
        textOne.indexOf(searchText) > -1 || textTwo.indexOf(searchText) > -1
      );
    },
    async addProduct() {
      await this.$axios
        .post("https://localhost:44367/Products", {
          name: this.product.name,
          amount: this.product.amount,
          price: this.product.price,
          password: this.product.password,
          producerName: this.product.producerName,
          categoryName: this.product.categoryName,
          description: this.product.description,
          brandTag: this.product.brandTag,
        }).then((response) => {
          this.productid =response.data
          this.addPhoto();
        }).catch((error) => {
          console.log(error);
        })
        
    },
    async addPhoto(){
      await this.$axios
        .post("https://localhost:44367/Products/AddPhoto/"+this.productid,this.photo)
    }
  },
  async asyncData({ $axios }) {
    var categories = await $axios.$get("https://localhost:44367/Categories");
    var producers = await $axios.$get("https://localhost:44367/Producers");
    return { producers, categories };
  },
  compute: {
    productid: "",
    photo: [],
  },
};
</script>