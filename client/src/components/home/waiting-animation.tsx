"use client";

import { AnimatePresence, motion } from "framer-motion";
import { redirect, usePathname } from "next/navigation";
// redirect
import { useEffect, useState } from "react";
import LoadingComponent from "../custom/loading-component";
import ErrorComponent from "../error/error-component";
import { IUserResponse } from "@/types/IResponse";

export default function WaitLayout({
	children,
	data,
}: {
	children: React.ReactNode;
	data: IUserResponse;
}) {
	const pathname = usePathname();
	const [isLoaded, setIsLoaded] = useState<boolean>(false);

	useEffect(() => {
		if (data) {
			setTimeout(() => {
				if (data.status !== 200) {
					if (data.status === 400) redirect(`/auth`);
					return <ErrorComponent message={data.message} />;
					// TODO : Create Error page to indicate when user is not found
				}
				setIsLoaded(true);
			}, 200);
		}
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
