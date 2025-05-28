"use client";

import { MotionButton, MotionCard } from "../animations/motion-wrapper";
import { Separator } from "../ui/separator";
import { Button } from "../ui/button";
import { formatDistanceToNow } from "date-fns";
import {
	Bookmark,
	CircleMinus,
	CirclePlus,
	Delete,
	Ellipsis,
	Heart,
	MessageCircle,
	Pencil,
	Share,
	Trash,
	UserRoundPlus,
} from "lucide-react";
import {
	DropdownMenu,
	DropdownMenuContent,
	DropdownMenuItem,
	DropdownMenuSeparator,
	DropdownMenuTrigger,
} from "../ui/dropdown-menu";
import { CustomAvatar } from "./custom-avatar";
import { useUser } from "@/hooks/contexts/useUser";
import { useAppContext } from "@/hooks/contexts/useAppContext";
import { useRouter } from "next/navigation";
import { CardContent, CardHeader } from "../ui/card";
import { IPost } from "@/types/post/IPost";
import Image from "next/image";
import MediaDialog from "../home/media-dialog";
import { useState } from "react";
import { Badge } from "../ui/badge";
import ImageLoader from "../loader/ImageLoader";
import usePostActions from "@/hooks/actions/usePostActions";

const Post = ({
	initPost,
	isViewMode = false,
	setSelectedPost,
}: {
	initPost: IPost;
	isViewMode?: boolean;
	setSelectedPost: React.Dispatch<React.SetStateAction<IPost>>;
}) => {
	const [hidden, setHidden] = useState(
		localStorage.getItem("hidden_posts")?.includes(initPost.post_id) || false
	);
	const { user } = useUser();
	const context = useAppContext();
	const router = useRouter();
	const [post, setPost] = useState<IPost>(initPost);

	const [loadingImages, setLoadingImages] = useState<Record<string, boolean>>(
		post.media.reduce((acc, media) => {
			acc[media.media_id] = true;
			return acc;
		}, {} as Record<string, boolean>)
	);

	const { handleShare, like, dislike, imageHandlers } = usePostActions({
		post: post,
		setPost: () => setPost,
	});

	return (
		<div>
			<Separator orientation="horizontal" className="w-full border-1 " />
			{hidden ? (
				<div className="md:space-y-4 space-y-0">
					<div className="p-4 justify-center  md:pb-0 shadow-lg">
						<div className="">
							<div className="text-muted-foreground italic px-2 py-1 flex w-full flex-row justify-between items-center">
								<p>{`This post has been hidden. `}</p>
								<Separator orientation="vertical" className="h-8" />
								<MotionButton>
									<Badge
										className="p-2 border outline bg-secondary text-background rounded-full cursor-pointer"
										onClick={() => {
											setHidden(!hidden);
											const hiddenPosts = JSON.parse(
												localStorage.getItem("hidden_posts") || "[]"
											);
											const updated = hiddenPosts.filter(
												(id: string) => id !== post.post_id
											);
											localStorage.setItem(
												"hidden_posts",
												JSON.stringify(updated)
											);
										}}
									>
										<CirclePlus className="w-4 h-4 text-muted-foreground" />
									</Badge>
								</MotionButton>
							</div>
						</div>
					</div>
				</div>
			) : (
				<div>
					<MotionCard className="space-y-4 w-full flex-wrap">
						<div className="">
							<CardHeader className="flex flex-row items-center justify-between p-4 px-0 md:px-4">
								<div className="flex flex-row gap-2 items-center">
									<CustomAvatar
										username={user?.username ?? ``}
										pfp_src={user?.pfp_src ?? ``}
										size={`h-9 w-9`}
									/>
									<div>
										<div className="flex flex-row items-center gap-1.5">
											<Button
												variant={`link`}
												onClick={() => {
													if (context?.page !== `profile`) {
														context?.setCurrPage({
															page: `profile`,
															username: post.user.username,
														});
														router.push(`/@${post.user.username}`);
													}
												}}
												className="font-semibold items-center cursor-pointer p-0"
											>
												{post.user.username.trim()}
											</Button>
											<div>
												<span>•</span>
											</div>
											<p className="text-sm text-muted-foreground">
												{formatDistanceToNow(new Date(post.created_at), {
													addSuffix: true,
												})}
											</p>
										</div>
									</div>
								</div>
								<div>
									<DropdownMenu>
										<DropdownMenuTrigger asChild disabled={isViewMode}>
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
											<DropdownMenuItem
												className="p-2 flex flex-row justify-between cursor-pointer"
												onClick={() => {
													const hiddenPosts = JSON.parse(
														localStorage.getItem("hidden_posts") || "[]"
													);
													if (!hiddenPosts.includes(post.post_id)) {
														hiddenPosts.push(post.post_id);
														localStorage.setItem(
															"hidden_posts",
															JSON.stringify(hiddenPosts)
														);
													}
													setHidden(true);
												}}
											>
												<p>Hide post</p>
												<CircleMinus className="h-4 w-4" />
											</DropdownMenuItem>
											{post.user.user_id !== user?.user_id && (
												<DropdownMenuItem className="p-2 flex flex-row justify-between cursor-pointer">
													<p>Follow {`${post.user.username}`}</p>
													<UserRoundPlus className="h-4 w-4" />
												</DropdownMenuItem>
											)}
											{post.user.user_id === user?.user_id && (
												<DropdownMenuItem className="p-2 flex flex-row justify-between cursor-pointer">
													<p>Edit post</p>
													<Pencil className="h-4 w-4" />
												</DropdownMenuItem>
											)}
											<DropdownMenuSeparator className="mt-2 mb-2" />
											{post.user.user_id === user?.user_id ? (
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
								<p className="whitespace-normal break-words mb-4">
									{post.content}
								</p>
								<div
									className={`flex flex-wrap ${
										post.media.length > 1
											? "gap-4 justify-start"
											: "justify-center"
									}`}
								>
									{post.media &&
										post.media.map((media) => (
											<div
												key={media.media_id}
												className="rounded-lg overflow-hidden mx-auto cursor-pointer"
											>
												<MediaDialog
													src={`${
														isViewMode
															? media.media_src
															: `http://localhost:5114${media.media_src}`
													}`}
													alt={`Media ${media.media_id}`}
													// key={idx}
													media_type={
														media.media_type === 0 ? "image" : "video"
													}
												>
													{media.media_type === 0 ? (
														<div>
															{loadingImages[media.media_id] && (
																<div className="absolute inset-0 flex items-center justify-center z-10 bg-black/10">
																	<ImageLoader />
																</div>
															)}
															<Image
																src={
																	isViewMode
																		? media.media_src
																		: `http://localhost:5114${media.media_src}`
																}
																priority={false}
																alt={`Media ${media.media_id}`}
																width={500}
																height={500}
																loading="lazy"
																onLoad={() =>
																	imageHandlers.onLoad(
																		media.media_id,
																		setLoadingImages
																	)
																}
																onError={() =>
																	imageHandlers.onError(
																		media.media_id,
																		setLoadingImages
																	)
																}
																quality={1}
																placeholder="empty"
																className="object-cover w-full rounded-xl shadow-md"
															/>
														</div>
													) : (
														<video
															src={`${
																isViewMode
																	? media.media_src
																	: `http://localhost:5114${media.media_src}`
															}`}
															controls
															className="w-full max-h-80 object-contain rounded-xl shadow-md"
														/>
													)}
												</MediaDialog>
											</div>
										))}
								</div>
							</CardContent>
						</div>
					</MotionCard>
					<div className="w-full flex flex-row justify-between gap-2 pt-4 pb-2 px-10">
						<div className="flex flex-row gap-4">
							<MotionButton className="flex items-center gap-1">
								<Button
									variant={`ghost`}
									size={`icon`}
									className="rounded-full cursor-pointer"
									disabled={isViewMode}
									onClick={() => {
										if (post.user.user_id !== user?.user_id) {
											if (post.is_liked_requester) {
												dislike();
												setPost((prev) => ({
													...prev,
													is_liked_requester: false,
													like_count: prev.like_count - 1,
												}));
											} else {
												like();
												setPost((prev) => ({
													...prev,
													is_liked_requester: true,
													like_count: prev.like_count + 1,
												}));
											}
										}
									}}
								>
									<Heart
										className="w-6 h-6 text-muted-foreground"
										style={{
											fill:
												post.is_liked_requester &&
												post.user.user_id !== user?.user_id
													? "currentColor"
													: "none",

											width: "1.2rem",
											height: "1.2rem",
										}}
									/>
								</Button>
								{post.like_count > 0 && <p>{post.like_count}</p>}
							</MotionButton>
							<MotionButton className="flex items-center gap-1">
								<Button
									variant={`ghost`}
									size={`icon`}
									className="px-4 rounded-full cursor-pointer"
									disabled={isViewMode}
									onClick={() => {
										setSelectedPost(post);
									}}
								>
									<MessageCircle
										className="w-6 h-6 text-muted-foreground"
										style={{
											width: "1.2rem",
											height: "1.2rem",
										}}
									/>
								</Button>
								{post.comment_count > 0 && (
									<p className="text-muted-foreground">{post.comment_count}</p>
								)}
							</MotionButton>
							<MotionButton>
								<Button
									variant={`ghost`}
									size={`icon`}
									className=" rounded-full cursor-pointer"
									disabled={isViewMode}
									onClick={() => handleShare(post.post_id)}
								>
									<Share
										className="w-6 h-6 text-muted-foreground"
										style={{
											width: "1.2rem",
											height: "1.2rem",
										}}
									/>
								</Button>
							</MotionButton>
							<MotionButton>
								<Button
									variant={`ghost`}
									className=" rounded-full cursor-pointer"
									size={`icon`}
									disabled={isViewMode}
								>
									<Bookmark
										className="w-6 h-6 text-muted-foreground"
										style={{
											width: "1.2rem",
											height: "1.2rem",
										}}
									/>
								</Button>
							</MotionButton>
						</div>
					</div>
				</div>
			)}
			{/* <Separator orientation="horizontal" className="w-full border-1 " /> */}
		</div>
	);
};

export default Post;
