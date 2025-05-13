import {
	MotionButton,
	MotionCard,
	PulsingStatusIndicator,
} from "@/animations/motion-wrapper";
import { CustomAvatar } from "./custom-avatar";
import {
	Tooltip,
	TooltipContent,
	TooltipProvider,
	TooltipTrigger,
} from "../ui/tooltip";
import {
	DropdownMenu,
	DropdownMenuTrigger,
	DropdownMenuItem,
	DropdownMenuContent,
	DropdownMenuSeparator,
	DropdownMenuSubContent,
	DropdownMenuSubTrigger,
	DropdownMenuSub,
} from "../ui/dropdown-menu";
import {
	ArrowDownAz,
	Bookmark,
	CalendarPlus,
	ChevronDown,
	CircleDot,
	CircleMinus,
	CirclePlus,
	Ellipsis,
	UserPlus,
	Users,
	UserSearch,
} from "lucide-react";
import { Button } from "../ui/button";
import {
	Menubar,
	MenubarContent,
	MenubarItem,
	MenubarMenu,
	MenubarSeparator,
	MenubarSub,
	MenubarSubContent,
	MenubarSubTrigger,
	MenubarTrigger,
} from "../ui/menubar";
import { useState } from "react";
import { Badge } from "../ui/badge";

const FriendsCard = () => {
	const [hidden, setHidden] = useState(false);

	if (hidden) {
		return (
			<div className="md:space-y-4 space-y-0">
				<div className="mb-4 justify-center  md:pb-0 rounded-3xl bg-secondary/45 dark:bg-secondary/45 border outline shadow-lg">
					<div className="p-4">
						<div className="text-muted-foreground italic px-2 py-1 flex w-full flex-row justify-between items-center">
							<p>This friends list has been hidden.</p>
							<Badge
								className="p-2 border outline bg-secondary text-background rounded-full cursor-pointer"
								onClick={() => setHidden(false)}
							>
								<CirclePlus className="w-4 h-4 text-muted-foreground" />
							</Badge>
						</div>
					</div>
				</div>
			</div>
		);
	}

	return (
		<div className="md:space-y-4 space-y-0">
			<div className="mb-4 justify-center pb-20 md:pb-0 rounded-3xl bg-secondary/45 dark:bg-secondary/45 border outline shadow-lg">
				<div className="p-6">
					<div className="flex items-center justify-between w-full mb-4">
						<div></div>
						<span className="text-2xl font-bold">Friends List</span>
						<div>
							<DropdownMenu>
								<DropdownMenuTrigger asChild>
									<Button
										variant="outline"
										size={`icon`}
										className="cursor-pointer rounded-full relative"
									>
										<Ellipsis />
										<PulsingStatusIndicator />
									</Button>
								</DropdownMenuTrigger>
								<DropdownMenuContent className="flex flex-col w-52 px-2 py-4 ">
									<DropdownMenuItem className="p-2 flex flex-row justify-between cursor-pointer">
										<p>View All Friends</p>
										<Users />
										{/* icon */}
									</DropdownMenuItem>
									<DropdownMenuSub>
										<DropdownMenuSubTrigger className="p-2 flex flex-row justify-between cursor-pointer">
											Sort by
										</DropdownMenuSubTrigger>
										<DropdownMenuSubContent className="w-44">
											<DropdownMenuItem className="p-2 flex flex-row justify-between cursor-pointer">
												Recently Added
												<CalendarPlus />
											</DropdownMenuItem>
											<DropdownMenuItem className="p-2 flex flex-row justify-between cursor-pointer">
												Alphabetical
												<ArrowDownAz />
											</DropdownMenuItem>
											<DropdownMenuItem className="flex flex-col justify-between items-center gap-1">
												<div className="flex flex-row justify-between cursor-pointer w-full text-muted-foreground">
													Online first
													<CircleDot />
												</div>
												<p className="text-muted-foreground text-sm">
													(coming soon)
												</p>
											</DropdownMenuItem>
										</DropdownMenuSubContent>
									</DropdownMenuSub>
									<DropdownMenuItem className="p-2 flex flex-row justify-between cursor-pointer">
										<p>Pending Requests</p>
										<UserPlus />
									</DropdownMenuItem>
									<DropdownMenuItem className="p-2 flex flex-row justify-between cursor-pointer">
										<p>Find Friends</p>
										<UserSearch />
										{/* icon */}
									</DropdownMenuItem>
									<DropdownMenuItem
										className="p-2 flex flex-row justify-between cursor-pointer"
										onClick={() => setHidden(true)}
									>
										<p>Hide Friends list</p>
										<CircleMinus className="h-4 w-4" />
									</DropdownMenuItem>
								</DropdownMenuContent>
							</DropdownMenu>
						</div>
					</div>
					<div className="grid grid-cols-2  sm:grid-cols-4 gap-3">
						{Array.from({ length: 8 }).map((_, i) => (
							<MotionButton
								className=" flex items-center justify-center flex-col cursor-pointer"
								key={i}
							>
								<div className="w-44 text-center flex flex-col justify-center items-center">
									<img
										src="https://th.bing.com/th/id/R.b2b34517339101a111716be1c203f354?rik=e5WHTShSpipi3Q&pid=ImgRaw&r=0"
										alt="User pic"
										className="h-40 w-40 rounded-lg object-cover mx-auto"
									/>

									<TooltipProvider>
										<Tooltip>
											<TooltipTrigger asChild>
												<p className="mt-2 w-[90%]  truncate text-center">
													@petarYankovasdadasddadsad
												</p>
											</TooltipTrigger>
											<TooltipContent>
												<p>petarYankovasdadasddadsad</p>
											</TooltipContent>
										</Tooltip>
									</TooltipProvider>
									<p className="text-sm text-muted-foreground text-center">
										20 mutual friends
									</p>
								</div>
							</MotionButton>
						))}
					</div>
					<div className="flex items-center justify-center">
						<Button variant="link" className="mt-4 text-sm cursor-pointer ">
							See more
							<ChevronDown />
						</Button>
					</div>
				</div>
			</div>
		</div>
	);
};

export default FriendsCard;
