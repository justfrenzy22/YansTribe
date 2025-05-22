"use client";

import { AnimatePresence, motion } from "framer-motion";
// import Link from "next/link";
import { redirect, usePathname } from "next/navigation";
import { useEffect, useState } from "react";
import LoadingComponent from "../custom/loading-component";
import { IBaseUser } from "@/types/IBaseUser";

export default function WaitLayout({
	data,
	children,
}: {
	data: {
		message: string;
		status: number;
		user: IBaseUser | null;
	};
	children: React.ReactNode;
}) {
	const pathname = usePathname();
	const [isLoaded, setIsLoaded] = useState<boolean>(false);

	useEffect(() => {
		setTimeout(() => {
			if (data.status === 200 && data.user !== null) {
				redirect("/");
			}
			setIsLoaded(true);
		}, 200);
	}, [data]);

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
					{children}
				</motion.div>
			)}
		</AnimatePresence>
	);
}
