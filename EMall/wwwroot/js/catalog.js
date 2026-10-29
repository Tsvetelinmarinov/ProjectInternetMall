(() => {
    // Наклон и светлина само на устройства с мишка и без ограничено движение
    const fine = matchMedia('(hover: hover) and (pointer: fine)').matches;
    const calm = matchMedia('(prefers-reduced-motion: reduce)').matches;
    if (!fine || calm) return;

    document.querySelectorAll('.cat-card').forEach(card => {
        card.addEventListener('pointermove', e => {
            const r = card.getBoundingClientRect();
            const x = e.clientX - r.left;
            const y = e.clientY - r.top;
            card.style.setProperty('--mx', `${x}px`);
            card.style.setProperty('--my', `${y}px`);
            card.style.setProperty('--ry', `${(x / r.width - .5) * 8}deg`);
            card.style.setProperty('--rx', `${(.5 - y / r.height) * 8}deg`);
        });

        card.addEventListener('pointerleave', () => {
            card.style.setProperty('--rx', '0deg');
            card.style.setProperty('--ry', '0deg');
        });
    });
})();
