# Dokumentation – Refaktorering av ASP.NET Web API

## Inledning

Syftet med denna uppgift var att analysera ett befintligt ASP.NET Web API,
identifiera design- och arkitekturproblem samt refaktorera koden enligt
SOLID-principerna, Clean Code och Repository Pattern.

Målet har varit att skapa en mer strukturerad, testbar och lättförståelig lösning.

---

## 1. Identifierade design- och arkitekturproblem

Nedan beskrivs fem separata problem som identifierades i den ursprungliga koden.

---

### Problem 1: Controllers innehöll affärslogik

**Beskrivning:**  
Controllers ansvarade inte enbart för HTTP-hantering,
utan innehöll även affärslogik såsom order­skapande, lagerkontroller, cart-hantering och prisberäkningar.

**Bruten princip / anti-pattern:**
- Single Responsibility Principle (SRP)
- God Controller anti-pattern

**Åtgärd:**  
Affärslogiken flyttades till services (`OrderService`, `CartService`,
`ProductService`). Controllers är nu tunna och ansvarar endast för
HTTP-kommunikation.

---

### Problem 2: Hård koppling mellan klasser (`new`-anrop)

**Beskrivning:**  
Controllers skapade själva sina beroenden via `new`, vilket resulterade
i en stark koppling mellan klasser.

```csharp
var service = new CartService();
```

**Bruten princip:**  
- Dependency Inversion Principle (DIP)

**Åtgärd:**  
Dependency Injection infördes. Controllers och services beror nu på interfaces
som injiceras via konstruktorer.

---

### Problem 3: Affärslogik och datalagring blandades

**Beskrivning:**  
Data lagrades direkt i services via statiska listor och dictionaries.
Services fungerade därmed både som affärslogik och datalager.

**Bruten princip / problem:**  
- Single Responsibility Principle  
- Separation of Concerns  
- Repository Pattern saknades  

**Åtgärd:**  
Repository Pattern infördes. Varje entitet fick ett eget repository  
(`IProductRepository`, `IOrderRepository`, etc.).  
Services använder endast repository-interfaces.

---

### Problem 4: Blandning av DTOs och domänmodeller

**Beskrivning:**  
Request- och response-klasser låg ibland i controllers eller blandades
med domänmodeller.

**Bruten princip:**  
- Single Responsibility Principle  
- Clean Code-principer  

**Åtgärd:**  
DTOs flyttades till separata mappar.  
Domänmodeller används internt, DTOs används endast för API-kommunikation.

---

### Problem 5: Svagt typad kod

**Beskrivning:**  
Exempelvis användes `List<object>` för orderrader.

**Problem:**  
- Svår att förstå  
- Svår att testa  
- Risk för runtime-fel  

**Bruten princip:**  
- Clean Code  
- Indirekt brott mot Open/Closed Principle  

**Åtgärd:**  
Starkt typade modeller infördes, t.ex. `OrderItem`.

---

## 2. Sammanfattning av problem

| Problem | Bruten princip |
|------|---------------|
| Controllers med affärslogik | SRP |
| `new`-anrop i controllers | DIP |
| Persistens i services | SRP |
| Blandade DTOs/modeller | SRP |
| Svag typning | Clean Code |

---

## 3. Teststrategi

### Övergripande strategi

Tester är främst skrivna för **service-lagret**, eftersom det är där
affärslogiken finns. Detta gör att testerna fokuserar på affärsregler 
snarare än tekniska detaljer.

Controllers testas endast i begränsad omfattning, då de är tunna och
huvudsakligen vidarebefordrar anrop.

---

### Mock, Stub och Fake

- **Mock:** används för att verifiera beteende, till exempel att en metod
  anropas ett visst antal gånger.
- **Stub:** används för att returnera kontrollerad testdata till den kod som
  testas.
- **Fake:** in-memory repositories används som fungerande, enkla
  implementationer utan externa beroenden.

---

### Testomfattning

- **ProductService** – grundläggande affärslogik  
- **CartService** – hantering av kvantitet och rensning av cart  
- **OrderService** – affärskritiska regler:
  - tom cart  
  - otillräckligt lager  
  - lyckad order  
- **Controller-test** – ett test för att verifiera korrekt HTTP-hantering  

Totalt ca **8 enhetstester**, vilket ger god täckning utan att övertesta.

---

## 4. Valda design patterns och varför

### Repository Pattern

- Separera affärslogik och dataåtkomst  
- Ökad testbarhet  
- Lättare att byta datakälla  

---

### Dependency Injection

- Minskad koppling mellan klasser  
- Möjliggör mockning i tester  
- Följer Dependency Inversion Principle  

---

### Service Layer Pattern

- Samlar affärslogik på ett ställe  
- Gör controllers tunna  
- Förbättrar struktur och underhållbarhet  

---

## 5. Reflektion

### Vad blev bättre?

- Tydligare ansvarsfördelning  
- Mer testbar kod  
- Lättare att förstå och vidareutveckla  
- Affärsregler verifieras nu tydligt med enhetstester

---

### Vad skulle kunna förbättras ytterligare?

- Införa databas (EF Core)  
- Transaktionshantering vid order-skapande  
- Caching för bättre prestanda  
- Central felhantering via middleware  
- JWT-baserad autentisering  
- Mer avancerad validering  

---

## Avslutning

Efter refaktoreringen följer API:et i hög grad SOLID-principerna och Clean Code.
Lösningen är körbar, testad och utgör en stabil grund för vidare utveckling.
