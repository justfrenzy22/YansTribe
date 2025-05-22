"use client";

import { useTheme } from "next-themes";
import { useEffect, useState } from "react";
import { Button } from "../ui/button";
import { motion } from "framer-motion";
import { Moon, Sun } from "lucide-react";
import { MotionButton } from "../animations/motion-wrapper";

const ToggleTheme = () => {
	const { theme, setTheme } = useTheme();
	const [mounted, setMounted] = useState(false);

	useEffect(() => {
		setMounted(true);
	}, []);

	if (!mounted) {
		return (
			<Button
				variant={`ghost`}
				size={`icon`}
				className="h-9 w-9 rounded-full"
			/>
		);
	}

	return (
		<MotionButton>
			<Button
				variant={`ghost`}
				size={`icon`}
				className="h-9 w-9 rounded-full cursor-pointer"
				onClick={() => setTheme(theme === "dark" ? "light" : "dark")}
			>
				{theme === "dark" ? (
					<motion.div
						key={`sun`}
						initial={{ y: -20, opacity: 0 }}
						animate={{ y: 0, opacity: 1 }}
						exit={{ y: 20, opacity: 0 }}
						transition={{ duration: 0.2 }}
					>
						<Sun className="h-5 w-5" />
					</motion.div>
				) : (
					<motion.div
						key={`moon`}
						initial={{ y: -20, opacity: 0 }}
						animate={{ y: 0, opacity: 1 }}
						exit={{ y: 20, opacity: 0 }}
						transition={{ duration: 0.2 }}
					>
						<Moon className="h-5 w-5" />
					</motion.div>
				)}
			</Button>
		</MotionButton>
	);
};

export default ToggleTheme;
