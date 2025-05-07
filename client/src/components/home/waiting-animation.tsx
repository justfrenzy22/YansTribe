"use client";

import { AnimatePresence, motion } from "framer-motion";
import { redirect, usePathname } from "next/navigation";
import { useEffect, useState } from "react";
import LoadingComponent from "../custom/loading-component";
import { IBaseUser } from "@/types/IEssentialsUser";
import ErrorComponent from "../error/error-component";

export default function WaitLayout({
	children,
	data,
}: {
	children: React.ReactNode;
	data: {
		message: string;
		status: number;
		user: IBaseUser;
	};
}) {
	const pathname = usePathname();
	const [isLoaded, setIsLoaded] = useState<boolean>(false);

	useEffect(() => {
		if (data) {
			setTimeout(() => {
				setIsLoaded(true);
			}, 200);
		}
	}, [data]);

	if (data.status !== 200) {
		if (data.status === 400) redirect(`/auth`);
		return <ErrorComponent />;
		// TODO : Create Error page to indicate when user is not found
	}

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
