"use client";

import Link from "next/link";
import {
	Home,
	LogOut,
	Menu,
	MessageCircle,
	Search,
	Settings,
	User,
	X,
} from "lucide-react";
import { useState } from "react";
import { Button } from "../ui/button";
import { Input } from "../ui/input";
import {
	DropdownMenu,
	DropdownMenuItem,
	DropdownMenuLabel,
	DropdownMenuSeparator,
	DropdownMenuTrigger,
	DropdownMenuContent,
} from "../ui/dropdown-menu";
import ToggleTheme from "./toggle-theme";
import BottomNav from "./bottom-nav";
import { Drawer, DrawerTrigger } from "../ui/drawer";
import MobileNav from "./mobile-nav";
import { logout } from "@/app/actions";
import { useUser } from "@/hooks/useUser";
import { useAppContext } from "@/hooks/useAppContext";
import { AppContextValue } from "@/types/AppContextValue";
import { useRouter } from "next/navigation";
import { CustomAvatar } from "./custom-avatar";

export const Navbar = (): React.ReactElement => {
	const [searchOpen, isSearchOpen] = useState<boolean>(false);
	const context: AppContextValue | null = useAppContext();
	const user = useUser();
	const router = useRouter();
	const signOut = async () => await logout();

	return (
		<>
			<header className="sticky top-0 z-50 w-full border-b bg-background/95 backdrop-blur supports-[backdrop-filter]:bg-background/60 flex justify-center dark:bg-gray-950">
				<div className="container flex h-16 items-center w-full justify-between px-4 md:px-4 ">
					<div className="flex items-center gap-2 md:gap-4">
						{/* <Sheet> */}
						{/* <SheetTrigger asChild>
								<Button variant="ghost" size="icon" className="md:hidden">
									<Menu className="h-5 w-5" />
									<span className="sr-only">Toggle menu</span>
								</Button>
							</SheetTrigger>
							<SheetContent side="left" className="p-0">
								<MobileNav />
							</SheetContent>
						</Sheet> */}
						<Drawer>
							<DrawerTrigger asChild>
								<Button variant="ghost" size="icon" className="md:hidden">
									<Menu className="h-5 w-5" />
									<span className="sr-only">Toggle menu</span>
								</Button>
							</DrawerTrigger>
							<div className="mx-auto w-full max-w-sm">
								<MobileNav />
							</div>
						</Drawer>

						<div
							onClick={() => {
								if (context?.page !== `home`) {
									context?.setCurrPage({ page: `home`, username: `` });
									router.push(`/`);
								}
							}}
							className="flex items-center gap-2 cursor-pointer"
						>
							<div className="h-8 w-8 rounded-full bg-gradient-to-br from-blue-500 flex items-center justify-center text-white font-bold text-lg select-none">
								Y
							</div>
							<span className="hidden font-bold text-xl md:inline-block select-none">
								YansTribe
							</span>
						</div>
					</div>

					<div className="hidden md:flex flex-1 items-center justify-center px-4">
						<div className="w-full max-w-md">
							<div className="relative">
								<Search className="absolute left-2.5 top-2.5 h-4 w-4 text-muted-foreground" />
								<Input
									type="search"
									placeholder="Search..."
									className="w-full bg-muted pl-8 md:w-[300px] lg:w-[400px] rounded-full"
								/>
							</div>
						</div>
					</div>

					<div className="flex items-center gap-2">
						<Button
							variant="ghost"
							size="icon"
							className="md:hidden"
							onClick={() => isSearchOpen(!searchOpen)}
						>
							{searchOpen ? (
								<X className="h-5 w-5" />
							) : (
								<Search className="h-5 w-5" />
							)}
						</Button>

						{searchOpen && (
							<div className="absolute left-0 top-16 w-full bg-background p-4 border-b md:hidden">
								<div className="relative">
									<Search className="absolute left-2.5 top-2.5 h-4 w-4 text-muted-foreground" />
									<Input
										type="search"
										placeholder="Search..."
										size={context?.isMobile ? "sm" : "md"}
										className="w-full pl-8 rounded-full"
										autoFocus
									/>
								</div>
							</div>
						)}

						<nav className="flex items-end w-full gap-1">
							<Button variant="ghost" size="icon" asChild>
								<Link href="/">
									<Home className="h-5 w-5" />
									<span className="sr-only">Home</span>
								</Link>
							</Button>
							<Button
								onClick={() => {
									if (context?.page !== `messages`) {
										context?.setCurrPage({
											page: `messages`,
											username: ``,
										});
										router.push(`/messages`);
									}
								}}
								variant="ghost"
								size="icon"
								// asChild
							>
								<MessageCircle className="h-5 w-5" />
								<span className="sr-only">Messages</span>
							</Button>
							<ToggleTheme />
							<DropdownMenu>
								<DropdownMenuTrigger asChild>
									<Button variant="ghost" size="icon" className="rounded-full">
										<CustomAvatar
											username={user?.username || ``}
											pfp_src={user?.pfp_src || ``}
											size={`h-8 w-8`}
										/>
									</Button>
								</DropdownMenuTrigger>
								<DropdownMenuContent align="end">
									<DropdownMenuLabel className="flex flex-row items-center gap-2 px-2">
										<CustomAvatar
											username={user?.username || ``}
											pfp_src={user?.pfp_src || ``}
											size={`h-8 w-8`}
										/>{" "}
										{user?.username}
									</DropdownMenuLabel>
									<DropdownMenuSeparator />
									<DropdownMenuItem
										className="flex justify-between items-center w-full cursor-pointer"
										onClick={() => {
											if (context?.page !== `profile`) {
												context?.setCurrPage({
													page: `profile`,
													username: user?.username || ``,
												});
												router.push(`/@${user?.username}`);
											}
										}}
									>
										Profile
										<User className="h-4 w-4" />
									</DropdownMenuItem>
									<DropdownMenuItem
										className="flex justify-between items-center w-full cursor-pointer"
										onClick={() => {
											if (context?.page !== `settings`) {
												context?.setCurrPage({
													page: `settings`,
													username: ``,
												});
												router.push(`/settings`);
											}
										}}
									>
										{/* <Link href="/settings" className="flex items-center"> */}
										<span>Settings</span>
										<Settings className="h-4 w-4" />
										{/* </Link> */}
									</DropdownMenuItem>
									<DropdownMenuItem
										className="flex justify-between items-center w-full cursor-pointer"
										onClick={async () => {
											try {
												await signOut();
											} catch (error) {
												console.error("Error during sign out:", error);
											}
										}}
									>
										Log out
										<LogOut className="h-4 w-4" />
									</DropdownMenuItem>
								</DropdownMenuContent>
							</DropdownMenu>
						</nav>
					</div>
				</div>
			</header>
			{context?.isMobile && <BottomNav selected="home" />}
		</>
	);
};
