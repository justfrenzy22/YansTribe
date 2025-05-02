import type { Metadata } from "next";
import "./globals.css";
import { ThemeProvider } from "next-themes";
import { Toaster } from "@/components/ui/sonner";

export const metadata: Metadata = {
	title: "YansTribe - Home",
	description:
		"YansTribe - Home page. YansTribe is social media platform for every kind of people.",
};

export default function RootLayout({
	children,
}: Readonly<{
	children: React.ReactNode;
}>) {
	return (
		<html lang="en" data-theme="default" suppressHydrationWarning>
			<head />
			<body className={`selection:bg-blue-400 bg-background `}>
				<ThemeProvider defaultTheme="dark">
					{children}
					<Toaster position="top-right" />
				</ThemeProvider>
			</body>
		</html>
	);
}
