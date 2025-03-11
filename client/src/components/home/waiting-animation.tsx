"use client";

import { AnimatePresence, motion } from "framer-motion";
import Link from "next/link";
import { redirect, usePathname } from "next/navigation";
import { useEffect, useState } from "react";

export default function WaitLayout({
	children,
	data,
}: {
	children: React.ReactNode;
	data: any;
}) {
	const pathname = usePathname();
	const [isLoaded, setIsLoaded] = useState<boolean>(false);

	useEffect(() => {
		if (data) {
            setTimeout(() => {
                setIsLoaded(true);
            }, 5000)
			// setIsLoaded(true);
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
				<motion.div
					key="loading"
					initial={{ opacity: 0 }}
					animate={{ opacity: 1 }}
					exit={{ opacity: 0 }}
					transition={{ duration: 1 }}
					className="flex justify-center items-center h-screen"
				>
					<div className="flex flex-col gap-2">
						<div className="flex flex-row gap-2 text-center">
							<div className="h-8 w-8 rounded-full bg-gradient-to-br from-blue-500 flex items-center justify-center text-white font-bold text-3xl">
								Y
							</div>
							<span className="font-bold text-3xl  inline-block">
								YansTribe
							</span>
						</div>
					</div>
					<div className="fixed bottom-[50px] ">
						<div className="flex flex-row text-center items-center justify-center gap-2">
							<p className="text-md ">by</p>
							<Link
								target="_blank"
								href="https://www.linkedin.com/in/petar-yankov-9835b623b/"
								className="font-bold text-xl text-blue-900 "
							>
								{" "}
								Petar Yankov
							</Link>
						</div>
					</div>
				</motion.div>
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
