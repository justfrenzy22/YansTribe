import type { Metadata } from "next";
import "./globals.css";
import { ThemeProvider } from "next-themes";
import { headers } from "next/headers";
import { Toaster } from "@/components/ui/sonner";
import { AppProvider } from "@/contexts/AppContext";

export const metadata: Metadata = {
	title: "YansTribe - Home",
	description:
		"YansTribe - Home page. YansTribe is social media platform for every kind of people.",
};

export default async function RootLayout({
	children,
}: Readonly<{
	children: React.ReactNode;
}>) {
	const userAgent = (await headers()).get(`user-agent`) || `user-agent`;
	const pathname = (await headers()).get(`x-invoke-path`) || `/`;

	return (
		<html lang="en" data-theme="default" suppressHydrationWarning>
			<head />
			<body className={`selection:bg-blue-400 bg-background `}>
				<ThemeProvider defaultTheme="dark">
					<AppProvider userAgent={userAgent} pathname={pathname}>
						{children}
						<Toaster position="top-right" />
					</AppProvider>
				</ThemeProvider>
			</body>
		</html>
	);
}
