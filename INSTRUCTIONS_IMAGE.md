# Instructions pour l'image de fond

## 📸 Placement de l'image

Pour que l'image de bibliothèque s'affiche en arrière-plan, placez votre fichier `img.jpg` dans le dossier suivant :

```
wwwroot/img.jpg
```

## 📁 Structure attendue

```
wwwroot/
├── img.jpg          ← Placez votre image ici
├── css/
│   └── site.css
├── js/
│   └── site.js
├── images/
└── uploads/
```

## 🎨 Recommandations pour l'image

- **Format** : JPG, PNG ou WebP
- **Nom** : `img.jpg` (ou modifiez le chemin dans `site.css` ligne 20)
- **Taille recommandée** : 1920x1080px ou plus
- **Poids** : Optimisez l'image pour le web (< 500KB de préférence)

## 🔧 Si vous utilisez un autre nom de fichier

Si votre image a un nom différent, modifiez le fichier `wwwroot/css/site.css` à la ligne 20 :

```css
background: linear-gradient(rgba(139, 69, 19, 0.85), rgba(101, 67, 33, 0.9)), url('/VOTRE_NOM_IMAGE.jpg') center center fixed;
```

## ✨ Effet appliqué

L'image sera :
- En arrière-plan fixe (ne défile pas avec la page)
- Recouverte d'un overlay marron semi-transparent pour améliorer la lisibilité
- Centrée et redimensionnée automatiquement pour couvrir tout l'écran

