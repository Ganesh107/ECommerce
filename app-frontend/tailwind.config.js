/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./src/**/*.{js,jsx}", 
  ],
  theme: {
    extend: {
      animation: {
        slide: 'slide 3s infinite',
      },
      keyframes: {
        move: {
          '0%': { transform: 'translateX(-50%)' }, 
          '100%': { transform: 'translateX(100%)' }, 
        },
      },
    }, 
  },
  plugins: [], 
};
