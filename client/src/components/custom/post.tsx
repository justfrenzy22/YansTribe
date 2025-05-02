"use client";

import { AvatarImage } from "@radix-ui/react-avatar";
import { MotionButton, MotionCard } from "../animations/motion-wrapper";
import { Avatar, AvatarFallback } from "../ui/avatar";
import { Card, CardContent, CardHeader } from "../ui/card";
import Link from "next/link";
import { motion } from "framer-motion";
import Image from "next/image";
import { Separator } from "../ui/separator";
import { Button } from "../ui/button";
// import CircleSvg from "@/public/filledHeart.svg";
import {
	Bookmark,
	ChevronRight,
	CircleMinus,
	Delete,
	DeleteIcon,
	Ellipsis,
	EyeClosed,
	Heart,
	MessageCircle,
	Pencil,
	Send,
	Share,
	Trash,
	UserRoundPlus,
} from "lucide-react";
import {
	ContextMenu,
	ContextMenuContent,
	ContextMenuItem,
	ContextMenuTrigger,
} from "../ui/context-menu";
import {
	DropdownMenu,
	DropdownMenuContent,
	DropdownMenuItem,
	DropdownMenuSeparator,
	DropdownMenuTrigger,
} from "../ui/dropdown-menu";
import { Label } from "../ui/label";
import { Input } from "../ui/input";
import Comment from "../home/comment";

const Post = ({ post }: any) => {
	return (
		<div>
			<Separator orientation="horizontal" className="w-full border-1 " />
			<MotionCard className="space-y-4">
				{/* bg-secondary border-none border-0 */}
				<div className="">
					<CardHeader className="flex flex-row items-center justify-between p-4 px-0 md:px-4 ">
						<div className="flex flex-row gap-2 items-center">
							<Avatar className="w-9 h-9">
								<AvatarImage src={`../${post.user.pfp_src}`} alt="User pic" />
								<AvatarFallback>
									YT
									{
										// alternative name
									}
								</AvatarFallback>
							</Avatar>
							<div>
								<div className="flex flex-row items-center gap-2">
									<Link
										href={`/profile/${post.user.user_id}`}
										className="font-semibold hover:underline items-center"
									>
										{post.user.username}
									</Link>
									<p className="text-sm text-muted-foreground items-center">
										{post === 0
											? "2 hours ago"
											: post === 1
											? "Yesterday"
											: "3 days ago"}
										{/* Logic for date calculation */}
									</p>
								</div>
							</div>
						</div>
						<div>
							<DropdownMenu>
								<DropdownMenuTrigger asChild>
									<Button
										variant="ghost"
										size={`icon`}
										className="cursor-pointer rounded-full"
									>
										<Ellipsis />
									</Button>
									{/* <Button variant="ghost" size="icon" className="rounded-full">
                    <MoreHorizontal className="h-5 w-5" />
                    <span className="sr-only">More options</span>
                  </Button> */}
								</DropdownMenuTrigger>
								<DropdownMenuContent className="flex flex-col w-48 px-2 py-4 ">
									<DropdownMenuItem className="p-2 flex flex-row justify-between cursor-pointer">
										<p>Save post</p>
										<Bookmark className="h-4 w-4" />
									</DropdownMenuItem>
									<DropdownMenuItem className="p-2 flex flex-row justify-between cursor-pointer">
										<p>Hide post</p>
										<CircleMinus className="h-4 w-4" />
									</DropdownMenuItem>
									{post.post_id % 2 !== 1 && (
										<DropdownMenuItem className="p-2 flex flex-row justify-between cursor-pointer">
											<p>Follow {`${post.user.username}`}</p>
											<UserRoundPlus className="h-4 w-4" />
										</DropdownMenuItem>
									)}
									{post.post_id % 2 === 1 && (
										<DropdownMenuItem className="p-2 flex flex-row justify-between cursor-pointer">
											<p>Edit post</p>
											<Pencil className="h-4 w-4" />
										</DropdownMenuItem>
									)}
									<DropdownMenuSeparator className="mt-2 mb-2" />
									{post.post_id % 2 === 1 ? (
										<DropdownMenuItem className="text-red-500 dark:text-red-400 flex justify-between p-2 cursor-pointer">
											<p>Delete post</p>
											<Trash className="h-4 w-4 bg-text-red-500 dark:bg-text-red-400" />
										</DropdownMenuItem>
									) : (
										<DropdownMenuItem className="text-red-500 flex flex-row justify-between dark:text-red-400 p-2 cursor-pointer">
											<p>Report post</p>
											<Delete className="h-4 w-4 bg-text-red-500 dark:bg-text-red-400" />
										</DropdownMenuItem>
									)}
								</DropdownMenuContent>
							</DropdownMenu>
						</div>
					</CardHeader>
					<CardContent className="pl-12 pt-0">
						<p className="mb-4">{post.content}</p>
						<motion.div
							initial={{ opacity: 0, scale: 0.95 }}
							animate={{ opacity: 1, scale: 1 }}
							transition={{ duration: 0.3, delay: 0.3 }}
							className="rounded-lg overflow-hidden"
						>
							<img
								src={`./code.jpg`}
								// src={`/placeholder.svg?height=400&width=600&text=${
								// post.post_id === 0
								// 	? "Your Post " + (post.post_id + 1)
								// 	: "Post Image " + (post.post_id + 1)
								// }`}
								alt={`Post trululu`}
								className="w-full object-cover rounded-xl shadow-md bg-primary outline"
							/>
						</motion.div>
					</CardContent>
				</div>
				{/* <Separator orientation="horizontal" /> */}
				{/* <Divider? */}
			</MotionCard>
			{/* buttons functionality */}
			<div className="w-full flex flex-row  justify-between gap-2 pt-4 px-10">
				<div className="flex flex-row gap-4">
					<MotionButton className="flex-1">
						<Button
							variant={`ghost`}
							className="px-4 rounded-full cursor-pointer"
						>
							{/* <filledHeart /> */}
							<Heart />
							<p>1</p>
						</Button>
					</MotionButton>
					<MotionButton className="flex-1">
						<Button
							variant={`ghost`}
							className="px-4 rounded-full cursor-pointer"
						>
							<MessageCircle />
							<p>15</p>
						</Button>
					</MotionButton>
					<MotionButton className="flex-1">
						<Button variant={`ghost`} className="p-5 rounded-xl cursor-pointer">
							{/* <filledHeart /> */}
							<Share />
						</Button>
					</MotionButton>
				</div>
				<MotionButton className="flex-1">
					<Button
						variant={`ghost`}
						className="px-4 rounded-full cursor-pointer"
						size={`icon`}
					>
						<Bookmark />
					</Button>
				</MotionButton>
			</div>
			{/* <div className="w-full flex flex-col gap-2 px-4 mb-4">
				<Comment />
				<Comment />
				<div className="w-full flex justify-center items-center">
					<MotionButton className="">
						<Button
							variant={`ghost`}
							className="px-4 rounded-full cursor-pointer"
						>
							<p>See all comments</p>
							<ChevronRight />
						</Button>
					</MotionButton>
				</div>
				<div className="pl-8">
					<Comment />
				</div>
			</div> */}

			{/* <div className="flex flex-row md:px-4 gap-2 px-2 w-full justify-center items-center mb-4">
				<Avatar className="w-9 h-9">
					<AvatarImage src={`../${post.user.pfp_src}`} alt="User pic" />
					<AvatarFallback>
						YT
						{
							// alternative name
						}
					</AvatarFallback>
				</Avatar>
				<Input

					className="pl-10 border outline shadow-md rounded-xl"
					// className="pl-10 border outline shadow-md rounded-lg bg-secondary"
					placeholder="Comment something..."
				/>
				<Button
					variant="ghost"
					size={`icon`}
					className="cursor-pointer rounded-full"
				>
					<Send className="" />
				</Button>
			</div> */}

			{/* <Separator orientation="horizontal" className="w-full border-1 my-4" /> */}
			{/* //{" "} */}
			{/* <Card className="bg-primary w-full">
				// <a></a>
				//{" "}
			</Card> */}
		</div>
	);
};

export default Post;
