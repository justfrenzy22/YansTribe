"use client";

import { motion } from "framer-motion";
import Image from "next/image";

const Brand = () => {
	return (
		<div>
			<motion.div
				initial={{ opacity: 0, x: -50 }}
				animate={{ opacity: 1, x: 0 }}
				transition={{ duration: 0.5 }}
				className="hidden md:flex md:1/2 flex-col items-center justify-center text-center"
			>
				<div className="max-w-md">
					<motion.div
						initial={{ scale: 0.8, opacity: 0 }}
						animate={{ scale: 1, opacity: 1 }}
						transition={{ delay: 0.2, duration: 0.5 }}
						className="flex justify-center items-center"
					>
						<Image
							src={"/logo.png"}
							alt="YansTribe logo"
							width={300}
							height={300}
							className="mb-6"
						/>
					</motion.div>
					<motion.h1
						initial={{ y: 20, opacity: 0 }}
						animate={{ y: 0, opacity: 1 }}
						transition={{ delay: 0.3, duration: 0.5 }}
						className="text-3xl font-bold mb-4"
					>
						Connect with friends and the world around you
					</motion.h1>
					<motion.p
						initial={{ y: 20, opacity: 0 }}
						animate={{ y: 0, opacity: 1 }}
						transition={{ delay: 0.4, duration: 0.5 }}
						className="text-gray-600 dark:text-gray-400"
					>
						Join YansTribe today to share photos, send messages, and stay
						updated with friends and family.
					</motion.p>
				</div>
			</motion.div>
		</div>
	);
};

export default Brand;
