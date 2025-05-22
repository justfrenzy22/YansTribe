"use client";

import { motion, AnimatePresence } from "framer-motion";
import type { ReactNode } from "react";

interface MotionWrapperProps {
	children: ReactNode;
	className?: string;
	onClick?: () => void;
}

interface MotionWrapperLoading extends MotionWrapperProps {
	children: ReactNode;
	className?: string;
	isLoading: boolean;
}

export function MotionWrapper({ children, className }: MotionWrapperProps) {
	return (
		<AnimatePresence mode="wait">
			<motion.div
				initial={{ opacity: 0, y: 20 }}
				animate={{ opacity: 1, y: 0 }}
				exit={{ opacity: 0, y: 20 }}
				transition={{ duration: 0.3 }}
				className={className}
			>
				{children}
			</motion.div>
		</AnimatePresence>
	);
}

export function MotionCard({
	children,
	className,
	delay = 0,
}: MotionWrapperProps & { delay?: number }) {
	return (
		<motion.div
			initial={{ opacity: 0, y: 20 }}
			animate={{ opacity: 1, y: 0 }}
			transition={{ duration: 0.3, delay }}
			className={className}
		>
			{children}
		</motion.div>
	);
}

export function MotionFade({ children, className }: MotionWrapperProps) {
	return (
		<motion.div
			initial={{ opacity: 0 }}
			animate={{ opacity: 1 }}
			exit={{ opacity: 0 }}
			transition={{ duration: 0.2 }}
			className={className}
		>
			{children}
		</motion.div>
	);
}

export function MotionLoadingButton({
	children,
	className,
	isLoading,
}: MotionWrapperLoading) {
	return (
		<motion.div
			className={className}
			whileHover={{ scale: isLoading ? 1 : 1.02 }}
			whileTap={{ scale: isLoading ? 1 : 0.98 }}
		>
			{children}
		</motion.div>
	);
}

export function MotionButton({ children, className, onClick }: MotionWrapperProps) {
	return (
		<motion.div
			onClick={onClick}
			className={className}
			whileHover={{ scale: 1.02 }}
			whileTap={{ scale: 0.98 }}
		>
			{children}
		</motion.div>
	);
}

export function MotionScale({ children, className }: MotionWrapperProps) {
	return (
		<motion.div
			initial={{ opacity: 0, scale: 0.9 }}
			animate={{ opacity: 1, scale: 1 }}
			exit={{ opacity: 0, scale: 0.9 }}
			transition={{ duration: 0.2 }}
			className={className}
		>
			{children}
		</motion.div>
	);
}
export function PulsingStatusIndicator() {
	return (
		<motion.div
			className="h-2 w-2 ring-[2px] ring-background rounded-full bg-sky-500 absolute top-0 right-0"
			whileHover={{
				scale: [1, 1.2, 1],
				transition: {
					duration: 1.2,
					repeat: Infinity,
					ease: "easeInOut",
				},
			}}
		/>
	);
}
