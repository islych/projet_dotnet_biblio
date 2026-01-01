# Structure de la Base de Données - Héritage TPH

## 📊 Table Comptes (Table Per Hierarchy)

Avec l'héritage TPH (Table Per Hierarchy), **tous les comptes** (Admin et Utilisateur) sont stockés dans **une seule table** : `dbo.Comptes`

### Structure de la table Comptes

| Colonne | Type | Description |
|---------|------|-------------|
| Id | int | Clé primaire |
| Nom | nvarchar(100) | Nom de l'utilisateur |
| Email | nvarchar(255) | Email (unique) |
| MotDePasse | nvarchar(255) | Mot de passe |
| DateInscription | datetime2 | Date d'inscription |
| **Discriminator** | nvarchar(50) | **Type de compte : "Admin" ou "Utilisateur"** |
| Role | nvarchar(50) | Rôle (uniquement pour Utilisateur, NULL pour Admin) |

### Comment distinguer Admin et Utilisateur ?

Le champ **`Discriminator`** indique le type :
- `Discriminator = "Admin"` → C'est un administrateur
- `Discriminator = "Utilisateur"` → C'est un utilisateur standard

### Requêtes SQL utiles

#### Voir tous les comptes
```sql
SELECT * FROM dbo.Comptes;
```

#### Voir uniquement les utilisateurs
```sql
SELECT * FROM dbo.Comptes 
WHERE Discriminator = 'Utilisateur';
```

#### Voir uniquement les admins
```sql
SELECT * FROM dbo.Comptes 
WHERE Discriminator = 'Admin';
```

#### Compter les utilisateurs par type
```sql
SELECT Discriminator, COUNT(*) as Nombre
FROM dbo.Comptes
GROUP BY Discriminator;
```

## ⚠️ Important

- **Il n'y a PAS de tables séparées** "Admin" et "Utilisateur"
- Tous les comptes sont dans la table **`Comptes`**
- Le champ `Discriminator` permet de distinguer les types
- Le champ `Role` est utilisé uniquement pour les Utilisateurs (peut être NULL pour les Admins)

## 🔍 Vérification après inscription

Après avoir créé un nouveau compte, vérifiez dans SQL Server Management Studio :

1. Ouvrez la table `dbo.Comptes`
2. Vous devriez voir votre nouveau compte avec :
   - `Discriminator = 'Utilisateur'`
   - `Role = 'Utilisateur'` (ou la valeur que vous avez définie)
   - Les autres champs remplis (Nom, Email, MotDePasse, DateInscription)

Si le compte n'apparaît pas, vérifiez :
- Les logs de l'application pour voir s'il y a des erreurs
- Que l'inscription s'est bien terminée (message de succès)
- Que vous n'avez pas d'erreur de contrainte unique sur l'email

