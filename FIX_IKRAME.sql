-- Script SQL pour corriger le compte ikrame
-- Ce compte a été créé avec Discriminator = 'Utilisateur' mais devrait être 'Admin'

-- Option 1: Mettre à jour le Discriminator pour ikrame
UPDATE dbo.Comptes
SET Discriminator = 'Admin'
WHERE Email = 'ikrame@gmail.com' AND Discriminator = 'Utilisateur';

-- Option 2: Si vous voulez supprimer le Role (car les Admins n'ont pas de Role)
UPDATE dbo.Comptes
SET Role = NULL
WHERE Discriminator = 'Admin';

-- Vérifier le résultat
SELECT Id, Nom, Email, Discriminator, Role, DateInscription
FROM dbo.Comptes
WHERE Email = 'ikrame@gmail.com';

