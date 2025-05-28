import Link from "next/link";
import { Avatar, AvatarFallback, AvatarImage } from "@/components/ui/avatar";
import { Home, MessageCircle, Settings, User, Users } from "lucide-react";
// import { useState } from "react";
import { DrawerTitle, DrawerContent, DrawerFooter } from "../ui/drawer";
import { useUser } from "@/hooks/contexts/useUser";
import { Button } from "../ui/button";
import { useAppContext } from "@/hooks/contexts/useAppContext";
import { redirect } from "next/navigation";

type linksType = {
	label: "home" | "profile" | "messages" | "friends" | "settings";
	href: "/" | string | "/messages" | "/friends" | "/settings";
	icon: React.ReactNode;
};

const MobileNav = () => {
	// const [isSelected, setSelected] = useState<linksType["label"] | null>(null);

	const { user } = useUser();
	const context = useAppContext();

	const btns: linksType[] = [
		{
			label: "home",
			href: "/",
			icon: <Home className="h-5 w-5" />,
		},
		{
			label: "profile",
			href: `/@${user?.username ?? ""}`,
			icon: <User className="h-5 w-5" />,
		},
		{
			label: "messages",
			href: "/messages",
			icon: <MessageCircle className="h-5 w-5" />,
		},
		{
			label: "friends",
			href: "/friends",
			icon: <Users className="h-5 w-5" />,
		},
		{
			label: "settings",
			href: "/settings",
			icon: <Settings className="h-5 w-5" />,
		},
	];

	return (
		<DrawerContent>
			<div className="flex flex-col h-full  ">
				<DrawerTitle>
					<div className="border-b p-4">
						<Link href="/profile" className="flex items-center gap-3">
							<Avatar>
								<AvatarImage
									src="/placeholder.svg?height=40&width=40"
									alt="@user"
								/>
								<AvatarFallback>U</AvatarFallback>
							</Avatar>
							<div>
								<p className="font-medium">John Doe</p>
								<p className="text-sm text-muted-foreground">
									View your profile
								</p>
							</div>
						</Link>
					</div>
				</DrawerTitle>
				<nav className="flex-1 overflow-auto p-4 border-b ">
					<ul className="grid gap-1">
						{btns.map((btn) => (
							<li key={btn.label}>
								<Button
									variant={`link`}
									onClick={() => {
										if (context && context.page !== btn.label) {
											context.setCurrPage({
												page: btn.label,
												username:
													btn.label === `profile` ? user?.username ?? `` : ``,
											});
											redirect(btn.href);
										}
									}}
									className="flex items-center gap-3 rounded-lg px-3 py-2 hover:bg-accent cursor-pointer"
								>
									{btn.icon}
									<span>{btn.label}</span>
								</Button>
							</li>
						))}
					</ul>
				</nav>
				<DrawerFooter>
					<div className="p-4 text-sm text-muted-foreground">
						<div className="flex flex-col justify-center items-center gap-2">
							<div className="flex flex-row gap-4">
								<Link href="/about" className="hover:underline">
									About
								</Link>
								<Link href="/privacy" className="hover:underline">
									Privacy
								</Link>
							</div>
							<div className="flex flex-row gap-4">
								<Link href="/terms" className="hover:underline">
									Terms
								</Link>
								<Link href="/cookies" className="hover:underline">
									Cookies
								</Link>
							</div>
							<div className="flex flex-row gap-4">
								<Link href="/careers" className="hover:underline">
									Careers
								</Link>
								<Link href="/help" className="hover:underline">
									Help Center
								</Link>
							</div>
						</div>
						<p className="mt-4 text-xs">
							© 2025 YansTribe. All rights reserved.
						</p>
					</div>
				</DrawerFooter>
			</div>
		</DrawerContent>
	);
};

export default MobileNav;
