"use client";

import { useState } from "react";
import { Button } from "../ui/button";
import { Home, MessageCircle, Search, ShieldUser, User } from "lucide-react";
import { useUser } from "@/hooks/contexts/useUser";
import { useAppContext } from "@/hooks/contexts/useAppContext";
import { redirect } from "next/navigation";

type btnType = {
	label: "home" | "search" | "messages" | "admin" | "profile";
	href: "/" | "/search" | "/messages" | "/admin" | string;
	icon: React.ReactNode;
};

const BottomNav = ({ selected }: { selected: btnType["label"] }) => {
	const [, setSelected] = useState<btnType["label"] | null>(selected);
	// const isAdmin = useUser();

	const { user } = useUser();
	const context = useAppContext();

	const btns: btnType[] = [
		{
			label: "home",
			href: "/",
			icon: <Home className="h-5 w-5" />,
		},
		{
			label: "search",
			href: "/search",
			icon: <Search className="h-5 w-5" />,
		},
		{
			label: "messages",
			href: "/messages",
			icon: <MessageCircle className="h-5 w-5" />,
		},
		{
			label: "admin",
			href: "/admin",
			icon: <ShieldUser className="h-5 w-5" />,
		},
		{
			label: "profile",
			href: `/@${user?.username ?? ""}`,
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

	console.log(`isSelected`, context);

	return (
		// dark:bg-gray-950
		// border-gray-200 dark:border-gray-800
		<div className="fixed bottom-0 left-0 right-0 z-50 w-full md:hidden border-t border-l border-r rounded-full">
			<div
				className="flex items-center justify-around w-full h-16"
				onClick={handleClick}
			>
				{btns.map((btn) => (
					<Button
						key={btn.label}
						variant={`link`}
						onClick={() => {
							if (context && context.page !== btn.label) {
								context.setCurrPage({
									page: btn.label,
									username: btn.label === `profile` ? user?.username ?? `` : ``,
								});
								redirect(btn.href);
							}
						}}
						className="flex items-center gap-3 rounded-lg px-3 py-2 cursor-pointer"
					>
						{/* {btn.icon}
						<span>{btn.label}</span> */}
						{btn.icon}
						{context && context.page === btn.label && (
							<span className="ml-1 text-sm">{btn.label}</span>
						)}
					</Button>
				))}
				{/* </div> */}
			</div>
		</div>
	);
};

export default BottomNav;
