"use client";

import Link from "next/link";
import { Bell, Home, Menu, MessageCircle, Search, User, X } from "lucide-react";
import { useState } from "react";
import { Sheet, SheetContent, SheetTrigger } from "../ui/sheet";
import { Button } from "../ui/button";
import { Input } from "../ui/input";
import { Avatar, AvatarFallback, AvatarImage } from "../ui/avatar";
import {
	DropdownMenu,
	DropdownMenuItem,
	DropdownMenuLabel,
	DropdownMenuSeparator,
	DropdownMenuTrigger,
	DropdownMenuContent,
} from "../ui/dropdown-menu";
import { MobileNav } from "./mobile-nav";
import ToggleTheme from "./toggle-theme";
import { useDevice } from "@/contexts/DeviceContext";
import BottomNav from "./bottom-nav";
import { Drawer, DrawerTrigger, DrawerContent } from "../ui/drawer";

export const Navbar = () => {
	const [searchOpen, isSearchOpen] = useState<boolean>(false);
	const isMobile = useDevice();

	return (
		<>
			<header className="sticky top-0 z-50 w-full border-b bg-background/95 backdrop-blur supports-[backdrop-filter]:bg-background/60">
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

						<Link href="/" className="flex items-center gap-2">
							<div className="h-8 w-8 rounded-full bg-gradient-to-br from-blue-500 flex items-center justify-center text-white font-bold text-lg">
								Y
							</div>
							<span className="hidden font-bold text-xl md:inline-block">
								YansTribe
							</span>
						</Link>
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
										size={isMobile ? "sm" : "md"}
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
							<Button variant="ghost" size="icon" asChild>
								<Link href="/messages">
									<MessageCircle className="h-5 w-5" />
									<span className="sr-only">Messages</span>
								</Link>
							</Button>
							<ToggleTheme />
							<DropdownMenu>
								<DropdownMenuTrigger asChild>
									<Button variant="ghost" size="icon" className="rounded-full">
										<Avatar className="h-8 w-8">
											<AvatarImage
												src="/placeholder.svg?height=32&width=32"
												alt="@user"
											/>
											<AvatarFallback>U</AvatarFallback>
										</Avatar>
									</Button>
								</DropdownMenuTrigger>
								<DropdownMenuContent align="end">
									<DropdownMenuLabel>My Account</DropdownMenuLabel>
									<DropdownMenuSeparator />
									<DropdownMenuItem>
										<Link href="/profile" className="flex items-center">
											<User className="mr-2 h-4 w-4" />
											<span>Profile</span>
										</Link>
									</DropdownMenuItem>
									<DropdownMenuItem>Settings</DropdownMenuItem>
									<DropdownMenuSeparator />
									<DropdownMenuItem>Log out</DropdownMenuItem>
								</DropdownMenuContent>
							</DropdownMenu>
						</nav>
					</div>
				</div>
			</header>
			{isMobile && <BottomNav selected="home" />}
		</>
	);
};
