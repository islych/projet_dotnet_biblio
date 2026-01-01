// Bibliothèque Web - Améliorations pour thème ancien
$(document).ready(function() {
    // ============================================
    // Gestion de la vidéo de fond
    // ============================================
      function initBackgroundVideo() {
        const video = $('.video-background video')[0];
        if (video) {
            // Essayer de démarrer la vidéo
            const playPromise = video.play();
            
            if (playPromise !== undefined) {
                playPromise.catch(error => {
                    console.log("Auto-play bloqué, ajout du bouton de lecture");
                    // Ajouter un bouton de lecture manuel
                    $('.video-background').append(`
                        <div class="video-play-button" style="
                            position: absolute;
                            top: 50%;
                            left: 50%;
                            transform: translate(-50%, -50%);
                            z-index: 2;
                            text-align: center;
                        ">
                            <button class="btn btn-lg btn-play-video" style="
                                background: rgba(206, 140, 165, 0.8);
                                border: 2px solid var(--or-pastel);
                                color: white;
                                padding: 1rem 2rem;
                                border-radius: 50px;
                                font-size: 1.2rem;
                                cursor: pointer;
                                transition: all 0.3s ease;
                            ">
                                <i class="fas fa-play me-2"></i>Lancer la vidéo
                            </button>
                            <p style="color: white; margin-top: 1rem; font-size: 0.9rem;">
                                Cliquez pour démarrer l'animation
                            </p>
                        </div>
                    `);
                    
                    $('.btn-play-video').on('click', function() {
                        video.muted = true; // Important pour certains navigateurs
                        video.play();
                        $(this).closest('.video-play-button').fadeOut(300);
                    });
                });
            }
            
            // Optimisation pour mobile
            if (/Mobi|Android/i.test(navigator.userAgent)) {
                video.setAttribute('playsinline', '');
                video.setAttribute('muted', '');
                video.setAttribute('autoplay', '');
            }
            
            // Surveiller les erreurs de chargement
            video.addEventListener('error', function(e) {
                console.error('Erreur de chargement vidéo:', e);
                // Fallback à une image de fond
                $('.video-background').css({
                    'background': 'linear-gradient(135deg, rgba(206, 140, 165, 0.8), rgba(43, 25, 46, 0.9))',
                    'background-image': 'none'
                }).html('');
            });
        }
    }
    
    initBackgroundVideo();
    
    // ============================================
    // AMÉLIORATION DU LOGO
    // ============================================
    
    function enhanceLogo() {
        $('.navbar-logo').on('error', function() {
            console.log('Logo introuvable, utilisation du fallback');
            $(this).attr('src', 'data:image/svg+xml;utf8,<svg xmlns="http://www.w3.org/2000/svg" width="60" height="60" viewBox="0 0 60 60"><circle cx="30" cy="30" r="30" fill="%23ce8ca5"/><text x="30" y="35" font-family="Arial" font-size="14" fill="white" text-anchor="middle">L</text></svg>');
        });
    }
    
    enhanceLogo();
    
    // ============================================
    // AMÉLIORATION DE LA NAVBAR MOBILE
    // ============================================
    
    // Gestion du bouton hamburger pour mobile
    function initMobileNavbar() {
        const $navbarToggler = $('.navbar-toggler');
        const $navbarCollapse = $('.navbar-collapse');
        
        if ($navbarToggler.length && $navbarCollapse.length) {
            // Gestion manuelle du clic sur le toggler
            $navbarToggler.on('click', function(e) {
                e.preventDefault();
                e.stopPropagation();
                
                // Basculer la classe 'show' sur la navbar-collapse
                $navbarCollapse.toggleClass('show');
                
                // Animation du bouton hamburger
                $(this).toggleClass('collapsed');
                
                // Empêcher le scroll du body quand le menu est ouvert
                if ($navbarCollapse.hasClass('show')) {
                    $('body').css('overflow', 'hidden');
                } else {
                    $('body').css('overflow', '');
                }
            });
            
            // Fermer le menu quand on clique en dehors
            $(document).on('click', function(e) {
                if (!$navbarCollapse.is(e.target) && 
                    $navbarCollapse.has(e.target).length === 0 && 
                    !$navbarToggler.is(e.target)) {
                    
                    $navbarCollapse.removeClass('show');
                    $navbarToggler.removeClass('collapsed');
                    $('body').css('overflow', '');
                }
            });
            
            // Fermer le menu quand on clique sur un lien
            $navbarCollapse.on('click', 'a.nav-link', function() {
                if ($(window).width() <= 767) {
                    $navbarCollapse.removeClass('show');
                    $navbarToggler.removeClass('collapsed');
                    $('body').css('overflow', '');
                }
            });
            
            // Gérer la redimensionnement de la fenêtre
            $(window).on('resize', function() {
                if ($(window).width() > 767) {
                    // Sur desktop, toujours afficher la navbar
                    $navbarCollapse.addClass('show');
                    $('body').css('overflow', '');
                } else {
                    // Sur mobile, cacher par défaut
                    $navbarCollapse.removeClass('show');
                    $navbarToggler.removeClass('collapsed');
                }
            });
        }
    }
    
    initMobileNavbar();
    
    // Reste du code JavaScript existant...
    // ============================================
    // Animations subtiles au chargement
    // ============================================
    
    // Animation d'apparition très subtile pour les cartes
    $('.card').each(function(index) {
        $(this).css('opacity', '0');
        setTimeout(() => {
            $(this).animate({ opacity: 1 }, 800);
        }, index * 100);
    });
    
    // ============================================
    // Amélioration des formulaires
    // ============================================
    
    $('.form-control, .form-select').on('focus', function() {
        $(this).addClass('focused');
    }).on('blur', function() {
        $(this).removeClass('focused');
    });
    
    // Validation en temps réel
    $('form').on('submit', function(e) {
        let isValid = true;
        $(this).find('input[required], select[required], textarea[required]').each(function() {
            if (!$(this).val()) {
                isValid = false;
                $(this).addClass('is-invalid');
                $(this).removeClass('is-valid');
            } else {
                $(this).removeClass('is-invalid');
                $(this).addClass('is-valid');
            }
        });
        
        if (!isValid) {
            e.preventDefault();
            showNotification('Veuillez remplir tous les champs obligatoires', 'warning');
        }
    });
    
    // ============================================
    // Notifications stylisées thème ancien
    // ============================================
    
    window.showNotification = function(message, type = 'info') {
        const alertClass = {
            'success': 'alert-success',
            'danger': 'alert-danger',
            'warning': 'alert-warning',
            'info': 'alert-info'
        }[type] || 'alert-info';
        
        const icon = {
            'success': '✓',
            'danger': '✗',
            'warning': '⚠',
            'info': 'ℹ'
        }[type] || 'ℹ';
        
        const notification = $(`
            <div class="alert ${alertClass} alert-dismissible fade show" 
                 role="alert" style="position: fixed; top: 20px; right: 20px; z-index: 9999; max-width: 350px; box-shadow: 0 4px 20px rgba(0,0,0,0.4); border-left-width: 4px;">
                <strong style="color: var(--or-patine);">${icon}</strong> ${message}
                <button type="button" class="btn-close" data-bs-dismiss="alert" style="filter: invert(1);"></button>
            </div>
        `);
        
        $('body').append(notification);
        
        // Auto-dismiss après 5 secondes
        setTimeout(function() {
            notification.fadeOut(400, function() {
                $(this).remove();
            });
        }, 5000);
    };
    
    // ============================================
    // Effets sur les boutons (plus subtils)
    // ============================================
    
    $('.btn').on('mousedown', function() {
        $(this).css('transform', 'translateY(1px)');
    }).on('mouseup mouseleave', function() {
        $(this).css('transform', '');
    });
    
    // ============================================
    // Confirmation de suppression stylisée
    // ============================================
    
    $('form[action*="Delete"]').on('submit', function(e) {
        if (!confirm('Êtes-vous sûr de vouloir supprimer cet élément ? Cette action est irréversible.')) {
            e.preventDefault();
        }
    });
    
    // ============================================
    // Effets sur les tables
    // ============================================
    
    $('.table tbody tr').on('mouseenter', function() {
        $(this).css('box-shadow', 'inset 4px 0 0 var(--or-patine)');
    }).on('mouseleave', function() {
        $(this).css('box-shadow', 'none');
    });
    
    // ============================================
    // Smooth scroll
    // ============================================
    
    $('a[href^="#"]').on('click', function(e) {
        const target = $(this.getAttribute('href'));
        if (target.length) {
            e.preventDefault();
            $('html, body').stop().animate({
                scrollTop: target.offset().top - 80
            }, 800);
        }
    });
    
    // ============================================
    // Auto-hide alerts
    // ============================================
    
    $('.alert:not(.alert-permanent)').each(function() {
        const $alert = $(this);
        setTimeout(function() {
            $alert.fadeOut(300, function() {
                $(this).remove();
            });
        }, 5000);
    });
    
    // ============================================
    // Effets sur les modals
    // ============================================
    
    $('.modal').on('show.bs.modal', function() {
        $(this).find('.modal-content').css({
            'opacity': '0',
            'transform': 'scale(0.95) translateY(-20px)'
        }).animate({
            'opacity': '1',
            'transform': 'scale(1) translateY(0)'
        }, 300);
    });
    
    // ============================================
    // Loading state pour les boutons (discret)
    // ============================================
    
    $('form').on('submit', function() {
        const $submitBtn = $(this).find('button[type="submit"]');
        if ($submitBtn.length) {
            const originalText = $submitBtn.html();
            $submitBtn.prop('disabled', true).html('<i class="fas fa-spinner fa-spin me-2"></i>Traitement...');
            
            // Réactiver après 15 secondes au cas où
            setTimeout(function() {
                $submitBtn.prop('disabled', false).html(originalText);
            }, 15000);
        }
    });
    
    // ============================================
    // Tooltip Bootstrap
    // ============================================
    
    if (typeof bootstrap !== 'undefined') {
        const tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
        tooltipTriggerList.map(function(tooltipTriggerEl) {
            return new bootstrap.Tooltip(tooltipTriggerEl, {
                template: '<div class="tooltip" role="tooltip"><div class="tooltip-inner" style="background: var(--bois-ancien); border: 1px solid var(--or-patine);"></div></div>'
            });
        });
    }
    
    // ============================================
    // Retour en haut de page stylisé
    // ============================================
    
    $(window).scroll(function() {
        if ($(this).scrollTop() > 200) {
            if ($('#back-to-top').length === 0) {
                $('body').append(`
                    <button id="back-to-top" class="btn" 
                        style="position: fixed; bottom: 30px; right: 30px; z-index: 1000; 
                               width: 50px; height: 50px; background: var(--bordeaux-profond); 
                               border: 1px solid var(--or-patine); color: var(--or-patine); 
                               font-size: 1.2rem; display: none; border-radius: 0;">
                        ↑
                    </button>
                `);
            }
            $('#back-to-top').fadeIn();
        } else {
            $('#back-to-top').fadeOut();
        }
    });
    
    $(document).on('click', '#back-to-top', function() {
        $('html, body').animate({scrollTop: 0}, 800);
    });
    
    // ============================================
    // Gestion des images défectueuses
    // ============================================
    
    $('img').on('error', function() {
        $(this).attr('src', 'data:image/svg+xml;utf8,<svg xmlns="http://www.w3.org/2000/svg" width="200" height="200" viewBox="0 0 200 200"><rect width="200" height="200" fill="%234A3A2A"/><text x="50%" y="50%" font-family="serif" font-size="16" fill="%23C9A24D" text-anchor="middle" dy=".3em">LIVRE</text></svg>');
    });
    
    // Lazy loading pour les images
    if ('IntersectionObserver' in window) {
        const imageObserver = new IntersectionObserver((entries, observer) => {
            entries.forEach(entry => {
                if (entry.isIntersecting) {
                    const img = entry.target;
                    if (img.dataset.src) {
                        img.src = img.dataset.src;
                        img.classList.remove('lazy');
                    }
                    imageObserver.unobserve(img);
                }
            });
        });
        
        document.querySelectorAll('img.lazy').forEach(img => {
            imageObserver.observe(img);
        });
    }
    
    // ============================================
    // Highlight des champs modifiés
    // ============================================
    
    $('input, select, textarea').on('change', function() {
        $(this).addClass('changed');
        setTimeout(() => {
            $(this).removeClass('changed');
        }, 2000);
    });
});

// ============================================
// Styles CSS injectés pour les effets
// ============================================

const style = document.createElement('style');
style.textContent = `
    .focused {
        box-shadow: 0 0 0 0.25rem rgba(201, 162, 77, 0.5) !important;
        border-color: var(--or-patine) !important;
    }
    
    .changed {
        border-color: var(--vert-bibliotheque) !important;
        box-shadow: 0 0 0 0.2rem rgba(30, 47, 42, 0.25) !important;
    }
    
    .is-invalid {
        border-color: var(--bordeaux-profond) !important;
    }
    
    .is-valid {
        border-color: var(--vert-bibliotheque) !important;
    }
    
    /* Animation pour les alertes */
    @keyframes slideInFromRight {
        from {
            transform: translateX(100%);
            opacity: 0;
        }
        to {
            transform: translateX(0);
            opacity: 1;
        }
    }
    
    .alert[style*="position: fixed"] {
        animation: slideInFromRight 0.3s ease-out;
    }
    
    /* Animation pour le spinner */
    .fa-spinner {
        animation: spin 1s linear infinite;
    }
    
    @keyframes spin {
        0% { transform: rotate(0deg); }
        100% { transform: rotate(360deg); }
    }
    
    /* Effet de profondeur pour les cartes */
    .card {
        transition: transform 0.3s ease, box-shadow 0.3s ease;
    }
`;
document.head.appendChild(style);