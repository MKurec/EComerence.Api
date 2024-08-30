## Instrukcja uruchomienia projektu EComerence

## Wymagania wstępne

- .NET Core SDK
- Node.js i npm (dla front-endu Vue.js)
- Python (dla skryptów Python)
- MSSQL

## Konfiguracja bazy danych

1. Zmień ciąg połączenia w pliku `appsettings.json` w projekcie `EComerence.Api` oraz w innych miejscach, gdzie jest to wymagane, na przykład w `ApplicationDbContext.cs`:optionsBuilder.UseSqlServer("Twój ciąg połączenia", b => b.MigrationsAssembly("EComerence.Api"));


2. Uruchom migracje bazy danych, aby utworzyć schemat bazy danych. Pomiń ten krok jeśli korzystasz z gotowego projektu z migracjami.


## Uruchomienie EComerence.Api

0. W IDE Visual Studio 2019 otwórz projekt `EComerence.Api` i uruchom go. lub
1. Otwórz terminal w katalogu projektu `EComerence.Api`.
2. Uruchom API przy użyciu polecenia: dotnet run

1. 
## Uruchomienie GTP3InsertionGenerator

1. Upewnij się, że masz poprawny klucz API dla OpenAI.
2. W pliku `GTP3InsertionGenerator/DataAccessLayer/Chatbot.cs` upewnij się, że klucz API jest poprawnie ustawiony:

## Uruchomienie ArtificialOrdersInput
1.	W pliku ArtificialOrdersInput/CreateOrders.cs upewnij się, że ścieżki do plików są poprawne.
2.	Uruchom projekt, aby wygenerować i wstawić zamówienia.

	1. ## Uruchomienie skryptów Python
	 Aby uruchomić plik `from surprise import Dataset, Reader, KN.py`, który zawiera kod Pythona, musisz wykonać następujące kroki:

1. **Zainstaluj wymagane biblioteki**: Upewnij się, że masz zainstalowane wszystkie wymagane biblioteki używane w skrypcie, takie jak [`pandas`], [`sqlalchemy`], [`sklearn`], [`numpy`],[`sqlalchemy_utils`], i inne. Możesz to zrobić za pomocą polecenia `pip` w terminalu:
   ```shell
   pip install pandas sqlalchemy sklearn numpy sqlalchemy_utils
   ```
   Jeśli używasz specyficznych wersji tych bibliotek, upewnij się, że zainstalujesz odpowiednie wersje.

2. **Skonfiguruj połączenie z bazą danych**: Skrypt wymaga połączenia z bazą danych SQL Server. Upewnij się, że masz zainstalowany odpowiedni sterownik ODBC i że parametry połączenia w zmiennej [`params`] są poprawnie skonfigurowane dla Twojej bazy danych.Powinna być to ta sama baza danych, którą skonfigurowałeś wcześniej dla projektu EComerence.


3. **Uruchom skrypt**: Otwórz terminal lub wiersz poleceń i przejdź do katalogu, w którym znajduje się plik skryptu. Następnie uruchom skrypt za pomocą interpretera Pythona:
   ```shell
   python "from surprise import Dataset, Reader, KN.py"
   ```
   Upewnij się, że ścieżka do pliku jest poprawna i że używasz nazwy pliku zgodnej z konwencjami systemu plików Twojego systemu operacyjnego (np. unikaj przecinków i spacji w nazwach plików).


   ## Uruchomienie front-endu
1. Instalacja zależności: Uruchom npm install w terminalu w katalogu głównym projektu, aby zainstalować wszystkie zależności wymienione w pliku package.json.

Uruchomienie projektu w trybie deweloperskim: Użyj komendy npm run dev w terminalu. Ta komenda uruchomi serwer deweloperski Nuxt.js, który automatycznie odświeża aplikację przy każdej zmianie kodu.

Otwarcie aplikacji w przeglądarce: Po uruchomieniu serwera deweloperskiego, otwórz przeglądarkę i przejdź do adresu URL wyświetlonego w terminalu, zazwyczaj jest to http://localhost:3000.

Uruchomienie projektu w trybie produkcyjnym: Jeśli chcesz uruchomić projekt w trybie produkcyjnym, użyj komendy npm run build, aby zbudować aplikację, a następnie npm start do uruchomienia serwera produkcyjnego.

Konfiguracja serwera deweloperskiego: W pliku nuxt.config.js zdefiniowano konfigurację serwera deweloperskiego, w tym proxy do API. Upewnij się, że serwer API jest uruchomiony i dostępny pod adresem skonfigurowanym w devServer.proxy.
