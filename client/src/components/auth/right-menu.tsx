"use client";

import { AnimatePresence, motion } from "framer-motion";
import { Button } from "../ui/button";
import { useState } from "react";
import { useTheme } from "next-themes";
import Login from "./login";
import changeColorTheme from "@/utils/changeColorTheme";
import { useColorTheme } from "@/utils/theme-provider";
import { DropdownMenu, DropdownMenuContent, DropdownMenuItem, DropdownMenuLabel, DropdownMenuSeparator, DropdownMenuTrigger } from "../ui/dropdown-menu";
import { Moon, Sun } from "lucide-react";
import { Card } from "../ui/card";
import Register from "./register";

const RightMenu = () => {
	const [isLogin, setLogin] = useState<boolean>(true);
	const { theme, setTheme } = useTheme();
	const { colorTheme, setColorTheme } = useColorTheme();
	// useEffect(() => {
	// 	document.documentElement.style.backgroundColor = "oklch(0.1 0.05 240)";
	// }, []);

	const handle = () => {
		changeColorTheme("dark-blue");
		// document.documentElement.style.backgroundColor = "oklch(0.1 0.05 240)";
	};
// outline dark:border-gray-800
	return (
		<Card
			className={`w-full md:w-1/2 max-w-lg  rounded-xl shadow-lg 
				
			`}
		>
			<DropdownMenu>
				<DropdownMenuTrigger asChild>
					<Button variant="outline" size="icon">
						{theme === "dark" ? (
							<Moon className="h-[1.2rem] w-[1.2rem]" />
						) : (
							<Sun className="h-[1.2rem] w-[1.2rem]" />
						)}
						<span className="sr-only">Toggle theme</span>
					</Button>
				</DropdownMenuTrigger>
				<DropdownMenuContent align="end">
					<DropdownMenuLabel>Appearance</DropdownMenuLabel>
					<DropdownMenuSeparator />
					<DropdownMenuLabel>Mode</DropdownMenuLabel>
					<DropdownMenuItem onClick={() => changeColorTheme("light")}>
						Light
					</DropdownMenuItem>
					<DropdownMenuItem onClick={() => changeColorTheme("dark")}>
						Dark
					</DropdownMenuItem>
					<DropdownMenuItem onClick={() => setTheme("system")}>
						System
					</DropdownMenuItem>
					<DropdownMenuSeparator />
					<DropdownMenuLabel>Color Theme</DropdownMenuLabel>
					<DropdownMenuItem onClick={() => changeColorTheme("blue-dark")}>
						Blue
					</DropdownMenuItem>
					<DropdownMenuItem onClick={() => changeColorTheme(`red-light`)}>
						Red
					</DropdownMenuItem>
				</DropdownMenuContent>
			</DropdownMenu>
			{/* // theme === "dark" ? "bg-gray-950" : "bg-white" */}
			<motion.div
				initial={{ opacity: 0, y: 20 }}
				animate={{ opacity: 1, y: 0 }}
				transition={{ duration: 0.5 }}
				className=" rounded-xl p-6 md:p-8 "
				// bg-white dark:bg-gray-950
			>
				{/* Form Toggle - Only show if we're not in the middle of registration */}
				<div className="flex mb-6 border-b dark:border-gray-800">
					<button
						onClick={() => setLogin(true)}
						className={`flex-1 py-3 font-medium text-center relative cursor-pointer ${
							isLogin
								? "text-primary"
								: "text-foreground"
						}`}
					>
						Login
						{isLogin && (
							<motion.div
								layoutId="activeTab"
								className="absolute bottom-0 left-0 right-0 h-0.5 bg-primary"
							/>
						)}
					</button>
					<button
						onClick={() => setLogin(false)}
						className={`flex-1 py-3 font-medium text-center relative cursor-pointer ${
							!isLogin
								? "text-primary"
								: "text-foreground"
						}`}
					>
						Register
						{!isLogin && (
							<motion.div
								layoutId="activeTab"
								className="absolute bottom-0 left-0 right-0 h-0.5 bg-primary"
							/>
						)}
					</button>
				</div>

				{/* Form Container */}
				<AnimatePresence mode="wait">
					{isLogin ? (
						<motion.div
							key="login"
							initial={{ opacity: 0, x: -20 }}
							animate={{ opacity: 1, x: 0 }}
							exit={{ opacity: 0, x: 20 }}
							transition={{ duration: 0.3 }}
						>
							<Login isLogin={isLogin} />
						</motion.div>
					) : (
						<motion.div
							key="register"
							initial={{ opacity: 0, x: 20 }}
							animate={{ opacity: 1, x: 0 }}
							exit={{ opacity: 0, x: -20 }}
							transition={{ duration: 0.3 }}
						>
						
							<Register setLogin={setLogin} />
						</motion.div>
					)}
				</AnimatePresence>
			</motion.div>
		</Card>
	);
};

export default RightMenu;
