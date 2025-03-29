const changeColorTheme = (color: string) => {
	const themeName = `${color}`;
	// console.log(document.querySelector(`html`));
	const html = document.querySelector(`html`);
	console.log(`themeColor ${themeName}`);
	if (html) {
		switch (themeName) {
			case `light`:
				html.removeAttribute(`data-theme`);
				html.style.colorScheme = themeName;
				break;
			case `dark`:
				html.setAttribute(`data-theme`, themeName);
				html.style.colorScheme = themeName;
				break;
			case `blue-dark`:
				html.setAttribute(`data-theme`, themeName);
				html.style.colorScheme = `dark`;
				break;
			case `red-light`:
				html.setAttribute(`data-theme`, themeName);
				html.style.colorScheme = `light`;
				break;
			default:
				console.log(`pishki`)
				break;
		}

		// if (themeName === `light`) {
		// 	// document.querySelector(`html`)?.removeAttribute(`data-theme`);
		// 	html.removeAttribute(`data-theme`);
		// } else {
		// 	html.setAttribute(`data-theme`, themeName);
		// }

		// html.style.colorScheme = themeName;
		// document.querySelector(`html`)?.setAttribute(`data-theme`, themeName);
	}
};

export default changeColorTheme;
