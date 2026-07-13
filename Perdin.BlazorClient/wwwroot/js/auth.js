window.authStorage = {
    setItem: (key, value) => localStorage.setItem(key, value),
    getItem: (key) => localStorage.getItem(key),
    removeItem: (key) => localStorage.removeItem(key)
};

window.scrollToTop = () => {
    const main = document.querySelector('main');
    if (main && main.parentElement) {
        main.parentElement.scrollTo({ top: 0, behavior: 'smooth' });
    }
};
