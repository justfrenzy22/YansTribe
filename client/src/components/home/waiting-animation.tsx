"use client";

import { AnimatePresence, motion } from "framer-motion";
import Link from "next/link";
import { redirect, usePathname } from "next/navigation";
import { useEffect, useState } from "react";
import LoadingComponent from "../custom/loading-component";
import { useTheme } from "next-themes";

export default function WaitLayout({
	children,
	data,
}: {
	children: React.ReactNode;
	data: any;
}) {
	const { theme } = useTheme();
	const pathname = usePathname();
	const [isLoaded, setIsLoaded] = useState<boolean>(false);

	useEffect(() => {
		if (data) {
			setTimeout(() => {
				setIsLoaded(true);
			}, 200);
		}
	}, [data]);

	if (data.status === 400) {
		redirect("/auth");
	}

	// useEffect(() => {
	// 	setTimeout(() => {
	// 		setIsLoaded(true);
	// 	}, 10000);
	// }, []);

	return (
		<AnimatePresence mode="wait">
			{!isLoaded ? (
				<LoadingComponent />
			) : (
				<motion.div
					key={pathname}
					initial={{ opacity: 0 }}
					animate={{ opacity: 1 }}
					exit={{ opacity: 0 }}
					transition={{ duration: 1 }}
				>
					{/* // dark:bg-[#111827] bg-[#F3F4F6] */}
					{children}
				</motion.div>
			)}
		</AnimatePresence>
	);
}
