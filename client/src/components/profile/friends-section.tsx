"use client";
import {
	MotionButton,
	PulsingStatusIndicator,
} from "@/components/animations/motion-wrapper";
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
	DropdownMenuSubContent,
	DropdownMenuSubTrigger,
	DropdownMenuSub,
} from "../ui/dropdown-menu";
import {
	ArrowDownAz,
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
import { useState } from "react";
import { Badge } from "../ui/badge";
import { useRouter } from "next/navigation";
import { useProfile } from "@/hooks/contexts/useProfile";

const FriendsCard = () => {
	const [hidden, setHidden] = useState(
		localStorage.getItem("hide_friends") === "true"
	);
	const { profile } = useProfile();
	const router = useRouter();

	if (hidden || profile?.friends_num === 0 || profile?.friends.length === 0) {
		return (
			<div className="md:space-y-4 space-y-0">
				<div className="mb-4 justify-center  md:pb-0 rounded-3xl bg-secondary/45 dark:bg-secondary/45 border outline shadow-lg">
					<div className="p-4">
						<div className="text-muted-foreground italic px-2 py-1 flex w-full flex-row justify-between items-center">
							<p>
								{hidden
									? `This friends list has been hidden.`
									: `You don't have any friends.`}
							</p>
							{hidden && (
								<MotionButton>
									<Badge
										className="p-2 border outline bg-secondary text-background rounded-full cursor-pointer"
										onClick={() => {
											setHidden(!hidden);
											localStorage.setItem("hide_friends", `${!hidden}`);
										}}
									>
										<CirclePlus className="w-4 h-4 text-muted-foreground" />
									</Badge>
								</MotionButton>
							)}
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
										onClick={() => {
											setHidden(!hidden);
											localStorage.setItem("hide_friends", `${!hidden}`);
										}}
									>
										<p>Hide Friends list</p>
										<CircleMinus className="h-4 w-4" />
									</DropdownMenuItem>
								</DropdownMenuContent>
							</DropdownMenu>
						</div>
					</div>
					<div className="grid grid-cols-2  sm:grid-cols-4 gap-3">
						{(profile?.friends.length as number) > 0 &&
							profile?.friends.map((friend, idx) => (
								<MotionButton
									onClick={() => router.push(`/@${friend.username}`)}
									className=" flex items-center justify-center flex-col cursor-pointer"
									key={idx}
								>
									<div className="w-44 text-center flex flex-col justify-center items-center">
										<img
											src={friend.pfp_src === "" ? "/pfp.png" : friend.pfp_src}
											alt="User pic"
											className="h-40 w-40 rounded-lg object-cover mx-auto"
										/>

										<TooltipProvider>
											<Tooltip>
												<TooltipTrigger asChild>
													<p className="mt-2 w-[90%]  truncate text-center">
														{friend.username}
													</p>
												</TooltipTrigger>
												<TooltipContent></TooltipContent>
											</Tooltip>
										</TooltipProvider>
										<p className="text-sm text-muted-foreground text-center">
											20 mutual friends
										</p>
									</div>
								</MotionButton>
							))}
					</div>
					{(profile?.friends.length as number) > 8 && (
						<div className="flex justify-center mt-4">
							<Button
								variant={`link`}
								className="text-muted-foreground"
								onClick={() => router.push(`/friends`)}
							>
								View All Friends
								<ChevronDown className="h-4 w-4 ml-2" />
							</Button>
						</div>
					)}
				</div>
			</div>
		</div>
	);
};

export default FriendsCard;
