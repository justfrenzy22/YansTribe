import {
	ThemeProviderProps,
	ThemeProvider as NextThemesProvider,
//   useTheme,
} from "next-themes";
import { createContext, useContext, useEffect, useState } from "react";

export type ColorTheme = `default` | `blue` | `red`;

type ThemeContextType = {
	colorTheme: ColorTheme;
	setColorTheme: (theme: ColorTheme) => void;
};

const ThemeContext = createContext<ThemeContextType>({
	colorTheme: `default`,
	setColorTheme: () => null,
});

export function ThemeProvider({ children, ...props }: ThemeProviderProps) {
	const [mounted, setMounted] = useState(false);
	const [colorTheme, setColorTheme] = useState<ColorTheme>("blue");

	useEffect(() => {
		setMounted(true);
		const savedColorTheme = localStorage.getItem("data-theme") as ColorTheme;

		// setColorTheme(savedColorTheme);
		document.documentElement.setAttribute("data-theme", savedColorTheme);
	}, []);

	// Save color theme to localStorage when it changes
	useEffect(() => {
		localStorage.setItem("data-theme", colorTheme);
		document.documentElement.setAttribute("data-theme", colorTheme);
	}, [colorTheme, mounted]);

	const value = {
		colorTheme,
		setColorTheme,
	};

	if (!mounted) {
		return <>{children}</>;
	}

	return (
		<ThemeContext.Provider value={value}>
			<NextThemesProvider {...props}>{children}</NextThemesProvider>
		</ThemeContext.Provider>
	);
}

export const useColorTheme = () => {
	const context = useContext(ThemeContext);
	if (context === undefined) {
		throw new Error("useColorTheme must be used within a ThemeProvider");
	}
	return context;
};
