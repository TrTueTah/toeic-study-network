document.querySelectorAll('.media-action-button').forEach(function(button) {
  button.addEventListener('click', function() {
    const icon = this.querySelector('i');
    const totalText = this.nextElementSibling;

    if (icon.classList.contains('bi-heart')) {
      icon.classList.remove('bi-heart');
      icon.classList.add('bi-heart-fill');
      this.classList.add('liked');
      totalText.classList.add('liked');
    } else {
      icon.classList.remove('bi-heart-fill');
      icon.classList.add('bi-heart');
      this.classList.remove('liked');
      totalText.classList.remove('liked');
    }
  });
});
