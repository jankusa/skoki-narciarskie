
# Statystyki Skoków Narciarskich

## Autorzy: 

- Jan Kusa
    - jankusa@student.agh.edu.pl
- Jan Majchrowicz
    - majchrowiczj@student.agh.edu.pl


## Opis

### Wstęp
Aplikacja internetowa *Statystyki Skoków Narciarskich* została napisana w technologii **ASP.NET core MVC** dla pasjonatów skoków narciarskich. Jest to narzędzie, które umożliwia dostęp do bazy danych, zawierającej informacje o skoczniach, skoczkach, konkursach oraz sezonach Puchary Świata. 

Dzięki aplikacji użytkownicy mogą pogłębić swoją wiedzę na temat skoków narciarskich, analizując różnorodne dane związane z tą dyscypliną. Tablice danych obejmują informacje o skoczniach, ich parametrach technicznych oraz rekordach. Ponadto, dostępne są dane dotyczące skoczków, obejmujące ich statystyki i inne informacje (m. in. wzrost, wiek itp.).

Projekt został stworzony z myślą o zapewnieniu użytkownikom łatwego dostępu do informacji na temat skoków narciarskich, co pozwala zarówno pasjonatom, jak i profesjonalistom, na poszerzanie swojej wiedzy oraz analizę danych związanych z tą dyscypliną sportu.

### Baza danych aplikacji

Baza danych zawarta w aplikacji składa się tabel:

- Skoczek
- Punktacja
- Konkurs
- Skocznia
- Sezon

powiązanych relacjami. Szczegóły na temat tych relacji zamieszczone zostały poniżej:
![Schemat relacji Bazy danych](database.png)

### Opis funkcjonalności

Oto przykłady niektórych funkcjonalności na stronie, przed tym wymagane jest jednak zalogowanie poprzez podanie **loginu** i **hasła**.

##### Konkursy w sezonie Pucharu Świata

Wyświetlana jest lista wszystkich konkursów, które odbyły w historii pucharu świata w formacie:
```
Sezon | Miejscowość | Data | Opis zawodów
```
Przykład użycia:

     Konkursy

##### Wyniki konkursu
Wyświetlane są wyniki wybrane konkursu w danym sezonie Pucharu Świata.
Przykład użycia:
    
    Konkursy -> Lillehammer -> Zobacz Wyniki

##### Skocznie 
Wyświetlana jest lista wszystkich skoczni, które znajdują się w bazie. Wyświetlane są informacje takie jak *nazwa*, *rozmiar*, *rekord*.

Przykład użycia:

    Skocznie

##### Wszyscy zawodnicy
Wyświetlana jest lista wszystkich zawodników w bazie wraz z informacjami o nich. 
Przykład użycia:

    Skoczkowie

##### Rekordowe noty zawodników
Wyświetlana jest lista zawodników oraz ich najwyższe zdobyte noty w dotychczasowych konkursach.  
Przykład użycia:

    Rekordy