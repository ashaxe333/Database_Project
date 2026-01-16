# Dokumentace projektu – Knihovní systém (Library Management System)

## 1. Základní informace
- **Název projektu:** Knihovní systém (Library Management System)
- **Autor:** Hynek Faktor
- **Kontaktní údaje:** h.faktor06@gmail.com
- **Datum vypracování:** 11.1. 2026
- **Škola:** Střední průmyslová škola elektrotechnická Ječná 30
- **Typ projektu:** Školní projekt

---

## 2. Účel aplikace
Aplikace slouží ke správě knihovního systému – evidenci uživatelů, knih, autorů, výpůjček a vazeb mezi knihami a autory. Umožňuje také import dat z CSV souborů nad některými tabulkami a zobrazení dat z databázových pohledů (views).

---

## 3. Požadavky uživatele (Functional Requirements)
- Přidání nové výpůjčky (Loan) mezi uživatelem a knihou
- Přidání nové knihy a autora v jedné transakci
- Import knih z CSV souboru
- Import autorů z CSV souboru
- Zobrazení aktivních výpůjček (view)
- Zobrazení přehledu plateb (view)

---

## 4. Architektura aplikace
Aplikace je postavena na **třívrstvé architektuře**:

1. **Prezentační vrstva (UI)** – Windows Forms (Form1)
2. **Aplikační vrstva (DAO)** – Data Access Object třídy
3. **Datová vrstva (Database + Models)** – MySQL databáze a modelové třídy

Použité návrhové vzory:
- **DAO (Data Access Object)** – každá tabulka má vlastní DAO třídu
- **Singleton** – DatabaseSingleton pro správu připojení k DB

### Schéma architektury (textově)
```
Form1 (UI)
   ↓
DAO vrstvy (UserDAO, BookDAO, LoanDAO, ...)
   ↓
DatabaseSingleton
   ↓
MySQL databáze
```

---

## 5. Popis běhu aplikace (Behaviorální popis)

### Příklad: Vytvoření nové výpůjčky
1. Uživatel vybere uživatele, knihu, status a datum
2. Klikne na tlačítko Submit
3. UI vytvoří objekt Loan
4. Zavolá se LoanDAO.Save()
5. Provede se INSERT do databáze

### Příklad: Přidání knihy a autora
1. Uživatel vyplní název knihy, rok vydání a jméno autora
2. Klikne na Submit
3. Otevře se databázová transakce
4. Vloží se kniha
5. Vloží se autor
6. Vloží se vazba do book_authors
7. Transakce se commitne

---

## 6. Použité knihovny, rozhraní a technologie (Non-functional requirements)

- **.NET Framework / .NET (Windows Forms)**
- **MySql.Data** – připojení k MySQL databázi
- **System.Configuration** – práce s config souborem
- **Regex** – validace vstupů

Databáze:
- **MySQL Server**

---

## 7. Právní a licenční informace
Projekt je školní a je určen pouze pro studijní účely. Použité knihovny jsou distribuovány pod svými licencemi (např. MySql.Data – GPL / komerční licence dle výrobce).

---

## 8. Konfigurace aplikace
Konfigurace se provádí pomocí souboru `App.config` / `WindowsFormsApp1.exe.config`.

Položky:
- **DataSource** – adresa databázového serveru
- **Database** – název databáze
- **Name** – uživatelské jméno
- **Password** – heslo

---

## 9. Instalace a spuštění
1. Nainstalovat MySQL Server
2. Importovat export databáze
3. Upravit App.config (přihlašovací údaje)
4. Spustit `WindowsFormsApp1.exe`

---

## 10. Chybové stavy
- **Chyba připojení k DB** – špatné údaje v configu
- **Foreign key error** – neexistující uživatel/kniha
- **Invalid name** – špatný formát jména autora
- **CSV import error** – špatný formát souboru

---

## 11. Testování a validace

### Provedené testy
- Přidání výpůjčky – OK
- Přidání knihy + autora – OK
- Import CSV – OK
- Zobrazení view – OK

### Výsledek
Aplikace splňuje zadané funkční požadavky.

---

## 12. Verze a známé chyby
- **v1.0** – první stabilní verze

Známé chyby:
- Není implementováno mazání přes UI
- Není ošetřen prázdný vstup u všech polí

---

## 13. Databázový model (E-R model – textově)

### Tabulky:
- **users (id, name, ...)**
- **books (id, title, published_year, isAvailable)**
- **authors (id, name)**
- **book_authors (id, book_id, author_id)**
- **loans (id, user_id, book_id, loan_date, return_date, status)**

Vazby:
- books : authors = M:N (přes book_authors)
- users : loans = 1:N
- books : loans = 1:N

---

## 14. Síť
Aplikace používá lokální připojení k databázi (localhost). Nepoužívá síťové služby.

---

## 15. Externí služby / HW
Nepoužívá žádné externí HW ani webové služby.

---

## 16. Import / Export

### Import knih (CSV)
Formát:
```
Title,PublishedYear,IsAvailable
```

### Import autorů (CSV)
Formát:
```
AuthorName