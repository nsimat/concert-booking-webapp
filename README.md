# 🎸 Concert Booking App

Une application web moderne de réservation de billets de concert, développée de bout en bout avec une architecture découplée.

**This personal project demonstrates modern full-stack development practices using ASP.NET Core 10 for the backend API and Angular 22 for the frontend UI.**

---

## 🚀 Fonctionnalités clés (Key Features)

*   **Authentification JWT** : Connexion et inscription sécurisées avec gestion des rôles (Client / Administrateur) et persistance des jetons côté Angular.
*   **Réservation en temps réel** : Sélection des places sur plan et mise à jour instantanée des disponibilités.
*   **Recherche & Filtres avancés** : Filtrage des concerts par artiste, genre musical, date et lieu.
*   **Paiement fictif** : Simulation d'une étape de paiement sécurisée pour valider la commande.
*   **Espace Admin** : Interface dédiée pour gérer les événements, ajouter des artistes et suivre les ventes.

## 🛠️ Technologies utilisées (Tech Stack)

*   **Frontend** : Angular 22, TypeScript, Tailwind CSS
*   **Backend** : ASP.NET Core 10, C#, Minimal APIs, JWT Authentication
*   **Base de données** : SQL Server (ou PostgreSQL), Entity Framework Core (EF Core 10)

---

## 🔐 Configuration de la Sécurité (JWT)

L'authentification est basée sur les JSON Web Tokens (JWT). Pour que l'API fonctionne, vous devez configurer la clé secrète dans le fichier `appsettings.json` du projet Backend :

```json
{
  "JwtSettings": {
    "Secret": "VOTRE_CLE_REELLEMENT_SECRETE_D_AU_MOINS_32_CARACTERES",
    "Issuer": "ConcertBookingBackend",
    "Audience": "ConcertBookingFrontend",
    "ExpiryInMinutes": 60
  }
}
```

*Note : Le frontend Angular intercepte automatiquement ce jeton via un `HttpInterceptor` pour l'ajouter au header `Authorization: Bearer <token>` de chaque requête.*

---

## 💾 Configuration de la Base de Données (EF Core)

Le projet utilise Entity Framework Core avec l'approche *Code-First*.

### 1. Mettre à jour la chaîne de connexion
Modifiez la section `ConnectionStrings` dans votre fichier `appsettings.json` :

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=ConcertBookingDb;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
```

### 2. Appliquer les Migrations
Exécutez les commandes suivantes dans votre terminal (dans le dossier du projet contenant votre `DbContext`) :

```bash
# Installer l'outil EF si ce n'est pas déjà fait
dotnet tool install --global dotnet-ef

# Générer/Appliquer la base de données
dotnet ef database update
```

---

## 💻 Installation et démarrage (Getting Started)

### Prérequis
*   [.NET 10 SDK](https://microsoft.com)
*   [Node.js](https://nodejs.org) (version compatible Angular 22)
*   Angular CLI (`npm install -g @angular/cli`)

### 1. Cloner le projet
```bash
git clone https://github.com
cd votre-depot
```

### 2. Lancer le Backend (ASP.NET Core)
```bash
cd backend
dotnet restore
dotnet ef database update
dotnet run
```
*L'API est accessible par défaut sur `http://localhost:5000` (ou `https://localhost:5001`).*

### 3. Lancer le Frontend (Angular)
```bash
cd ../frontend
npm install
ng serve
```
*Ouvrez votre navigateur sur `http://localhost:4200`.*

---

## 🎯 Objectifs d'apprentissage (Learning Goals)

Ce projet personnel a été initié pour approfondir mes compétences sur :
1.  La mise en place d'une architecture propre et scalable (Clean Architecture / CQRS).
2.  La maîtrise des nouveautés de **ASP.NET Core 10** et de **Angular 22**.
3.  La sécurisation des échanges et la gestion du cycle de vie des jetons JWT.

---

## 📝 Licence

Ce projet est sous licence MIT. Voir le fichier [LICENSE](LICENSE) pour plus de détails.
