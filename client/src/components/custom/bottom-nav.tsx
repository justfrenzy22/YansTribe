"use client";

import { useState } from "react";
import { Button } from "../ui/button";
import Link from "next/link";
import { Home, MessageCircle, Search, ShieldUser, User } from "lucide-react";

type btnType = {
	label: "home" | "search" | "messages" | "admin" | "profile";
	href: "/" | "/#search" | "/#messages" | "/#admin" | "/#profile";
	icon: React.ReactNode;
};

const BottomNav = ({ selected }: { selected: btnType["label"] }) => {
	const [isSelected, setSelected] = useState<btnType["label"] | null>(selected);
    // const isAdmin = useUser();
    
	const btns: btnType[] = [
		{
			label: "home",
			href: "/",
			icon: <Home className="h-5 w-5" />,
		},
		{
			label: "search",
			href: "/#search",
			icon: <Search className="h-5 w-5" />,
		},
		{
			label: "messages",
			href: "/#messages",
			icon: <MessageCircle className="h-5 w-5" />,
		},
		{
			label: "admin",
			href: "/#admin",
			icon: <ShieldUser className="h-5 w-5" />,
		},
		{
			label: "profile",
			href: "/#profile",
			icon: <User className="h-5 w-5" />,
		},
	];

	// Event Delegation
	const handleClick = (e: React.MouseEvent<HTMLDivElement>) => {
		const target = e.target as HTMLElement;
		console.log(target);
		const label = target.closest("a")?.getAttribute("data-label");
		console.log(label);
		if (label) {
			setSelected(label as btnType["label"]);
		}
	};

	return (
		// dark:bg-gray-950
		// border-gray-200 dark:border-gray-800
		<div className="fixed bottom-0 left-0 right-0 z-50 w-full md:hidden border-t border-l border-r rounded-full">
			<div
				className="flex items-center justify-around w-full h-16"
				onClick={handleClick}
			>
				{/* <div onClick={handleClick}> */}
				{btns.map((btn) => (
					<Button
						key={btn.href}
						variant={isSelected === btn.label ? "secondary" : "ghost"}
						data-label={btn.label}
						size={isSelected === btn.label ? `lg` : "icon"}
						className={`h-12 rounded-full ${
							isSelected === btn.label ? `w-auto px-4` : "w-12"
						}`}
						asChild
					>
						<Link href={btn.href}>
							{btn.icon}
							{isSelected === btn.label && (
								<span className="ml-1 text-sm">{btn.label}</span>
							)}
						</Link>
					</Button>
				))}
				{/* </div> */}
			</div>
		</div>
	);
};

export default BottomNav;
