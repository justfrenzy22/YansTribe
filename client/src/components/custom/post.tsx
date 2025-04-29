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
import { formatDistanceToNow } from "date-fns";
import {
	Bookmark,
	CircleMinus,
	Delete,
	Ellipsis,
	Heart,
	MessageCircle,
	Pencil,
	Send,
	Share,
	Trash,
	UserRoundPlus,
	X,
} from "lucide-react";
import {
	DropdownMenu,
	DropdownMenuContent,
	DropdownMenuItem,
	DropdownMenuSeparator,
	DropdownMenuTrigger,
} from "../ui/dropdown-menu";
import { useState } from "react";
import {
	Dialog,
	DialogTrigger,
	DialogContent,
	DialogTitle,
} from "../ui/dialog";

const Post = ({
	post,
	isViewMode = false,
}: {
	post: any;
	isViewMode?: boolean;
}) => {
	const [selectedMedia, setSelectedMedia] = useState<string | null>(null);

	return (
		<div>
			{/* Dialog for enlarged media */}

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
										{formatDistanceToNow(new Date(post.created_at), {
											addSuffix: true,
										})}
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
						<div
							className={`flex flex-wrap ${
								post.media.length > 1 ? "gap-4 justify-start" : "justify-center"
							}`}
						>
							{post.media &&
								post.media.map(
									(media: { type: string; url: string }, index: number) => (
										<div
											key={index}
											className="rounded-lg overflow-hidden max-w-md mx-auto cursor-pointer"
											onClick={() => {
												if (media.type === "image") {
													setSelectedMedia(media.url);
												}
											}}
										>
											<Dialog>
												{media.type === "image" ? (
													<DialogTrigger asChild>
														<img
															src={media.url}
															alt={`Media ${index + 1}`}
															className="w-full max-h-80 object-cover rounded-xl shadow-md"
														/>
													</DialogTrigger>
												) : (
													<DialogTrigger asChild>
														<video
															src={media.url}
															controls
															className="w-full max-h-80 object-contain rounded-xl shadow-md"
														/>
													</DialogTrigger>
												)}
												<DialogContent className="backdrop-blur-sm bg-transparent bg-opacity-50 p-4 flex justify-center items-center flex-col w-auto h-auto max-w-[95vw] max-h-[95vh]">
													<DialogTitle>Extended Media</DialogTitle>
													<div className="w-full rounded-lg shadow-lg p-4">
														{media.type === "image" ? (
															<img
																src={selectedMedia ?? "code.jpg"}
																alt="Enlarged media"
																className="rounded-lg shadow-lg  max-h-[90vh]"
																// TODO: fix the size of the image to be responsive
															/>
														) : (
															<video
																src={media.url}
																controls
																className="rounded-lg shadow-lg object-contain max-w-full max-h-[90vh]"
															/>
														)}
													</div>
												</DialogContent>
											</Dialog>
										</div>
									)
								)}
						</div>
					</CardContent>
				</div>
			</MotionCard>
			{/* buttons functionality */}
			<div className="w-full flex flex-row justify-between gap-2 pt-4 px-10">
				<div className="flex flex-row gap-4">
					<MotionButton className="flex-1">
						<Button
							variant={`ghost`}
							className="px-4 rounded-full cursor-pointer"
							disabled={isViewMode}
						>
							<Heart />
							<p>{post.likes > 0 ? post.likes : ""}</p>{" "}
						</Button>
					</MotionButton>
					<MotionButton className="flex-1">
						<Button
							variant={`ghost`}
							className="px-4 rounded-full cursor-pointer"
							disabled={isViewMode}
						>
							<MessageCircle />
							<p>{post.comments > 0 ? post.comments : ""}</p>{" "}
						</Button>
					</MotionButton>
					<MotionButton className="flex-1">
						<Button
							variant={`ghost`}
							className="p-5 rounded-xl cursor-pointer"
							disabled={isViewMode}
						>
							<Share />
						</Button>
					</MotionButton>
				</div>
				<MotionButton className="flex-1">
					<Button
						variant={`ghost`}
						className="px-4 rounded-full cursor-pointer"
						size={`icon`}
						disabled={isViewMode}
					>
						<Bookmark />
					</Button>
				</MotionButton>
			</div>
		</div>
	);
};

export default Post;
