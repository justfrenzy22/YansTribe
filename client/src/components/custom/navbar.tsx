"use client";

import {
	Bell,
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
import { useUser } from "@/hooks/contexts/useUser";
import { useAppContext } from "@/hooks/contexts/useAppContext";
import { AppContextValue } from "@/types/AppContextValue";
import { redirect, useRouter } from "next/navigation";
import { CustomAvatar } from "./custom-avatar";
import {
	MotionButton,
	PulsingStatusIndicator,
} from "../animations/motion-wrapper";
import { DropdownMenuItemIndicator } from "@radix-ui/react-dropdown-menu";
import useNotificationFriendActions from "@/hooks/actions/useNotificationFriendActions";

export const Navbar = (): React.ReactElement => {
	const [searchOpen, isSearchOpen] = useState<boolean>(false);
	const context: AppContextValue | null = useAppContext();
	const { user } = useUser();
	const router = useRouter();
	const { acceptFriend, declineFriend, isLoading } =
		useNotificationFriendActions();
	const signOut = async () => await logout();

	return (
		<>
			<header className="sticky top-0 z-50 w-full border-b bg-background/95 backdrop-blur supports-[backdrop-filter]:bg-background/60 flex justify-center dark:bg-gray-950">
				<div className="container flex h-16 items-center w-full justify-between px-4 md:px-4 ">
					<div className="flex items-center gap-2 md:gap-4">
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
							<MotionButton>
								<Button
									variant="ghost"
									size="icon"
									className="rounded-full cursor-pointer"
									onClick={() => {
										if (context?.page !== `home`) {
											context?.setCurrPage({ page: `home`, username: `` });
											router.push(`/`);
										}
									}}
								>
									{/* <Link href="/"> */}
									<Home className="h-6 w-6" />
									<span className="sr-only">Home</span>
									{/* </Link> */}
								</Button>
							</MotionButton>
							<MotionButton>
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
									className="rounded-full cursor-pointer"
								>
									<MessageCircle className="h-6 w-6" />
									<span className="sr-only">Messages</span>
								</Button>
							</MotionButton>
							<DropdownMenu>
								<DropdownMenuTrigger asChild>
									<div>
										<MotionButton>
											<Button
												variant="ghost"
												size="icon"
												className="rounded-full cursor-pointer"
											>
												<div className="relative">
													<Bell className="h-6 w-6" />
													{/* <PulsingStatusIndicator /> */}
													{(user?.notifications.friendNotifications?.length ??
														0) > 0 && <PulsingStatusIndicator />}
												</div>
												<span className="sr-only">Notifications</span>
											</Button>
										</MotionButton>
									</div>
								</DropdownMenuTrigger>
								<DropdownMenuContent
									align={context?.isMobile ? `start` : `end`}
								>
									<DropdownMenuLabel>Notifications</DropdownMenuLabel>
									<DropdownMenuSeparator />
									<DropdownMenuLabel>Friend Notifications</DropdownMenuLabel>
									{user?.notifications.friendNotifications?.map(
										(notification) => {
											return (
												<DropdownMenuItem
													key={notification.sender_id}
													className="flex justify-between items-center w-full cursor-pointer p-2 gap-4"
												>
													<div className="flex flex-row gap-2 items-center">
														<CustomAvatar
															username={notification.username || ``}
															pfp_src={notification.pfp_src || ``}
															size={`h-8 w-8`}
														/>
														<div className="flex flex-col">
															<p
																onClick={() =>
																	redirect(`/@${notification.username}`)
																}
															>
																{notification.username}
															</p>
														</div>
													</div>
													<div className="flex flex-row items-center gap-2">
														<MotionButton>
															<Button
																variant={`outline`}
																onClick={async () =>
																	await acceptFriend(notification.sender_id)
																}
																disabled={isLoading}
																className="cursor-pointer rounded-xl"
															>
																Confirm
															</Button>
														</MotionButton>
														<MotionButton>
															<Button
																variant={"destructive"}
																onClick={async () =>
																	await declineFriend(notification.sender_id)
																}
																disabled={isLoading}
																className="cursor-pointer rounded-xl"
															>
																Decline
															</Button>
														</MotionButton>
													</div>
												</DropdownMenuItem>
											);
										}
									)}
									<DropdownMenuItemIndicator />
								</DropdownMenuContent>
							</DropdownMenu>
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
											context?.setCurrPage({
												page: `profile`,
												username: user?.username || ``,
											});
											router.push(`/@${user?.username}`);
										}}
									>
										Profile
										<User className="h-6 w-6" />
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
										<Settings className="h-6 w-6" />
										{/* </Link> */}
									</DropdownMenuItem>
									<DropdownMenuItem
										className="flex justify-between items-center w-full cursor-pointer"
										onClick={async () => {
											try {
												await signOut();
												redirect("/auth");
											} catch (error) {
												console.error("Error during sign out:", error);
											}
										}}
									>
										Log out
										<LogOut className="h-6 w-6" />
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
