import { IPost } from "@/types/post/IPost";
import { Dialog, DialogContent, DialogTitle } from "../ui/dialog";
import { useUser } from "@/hooks/contexts/useUser";
import { MotionButton, MotionCard } from "../animations/motion-wrapper";
import { Button } from "../ui/button";
import {
	Bookmark,
	Ellipsis,
	Heart,
	Share,
	Sparkles,
	XIcon,
} from "lucide-react";
import usePostActions from "@/hooks/actions/usePostActions";
import { useAppContext } from "@/hooks/contexts/useAppContext";
import { CardHeader } from "../ui/card";
import { CustomAvatar } from "./custom-avatar";
import { useRouter } from "next/navigation";
import {
	DropdownMenu,
	DropdownMenuContent,
	DropdownMenuItem,
} from "../ui/dropdown-menu";
import { DropdownMenuTrigger } from "@radix-ui/react-dropdown-menu";

import { formatDistanceToNow } from "date-fns";
import {
	CircleMinus,
	Delete,
	MessageCircle,
	Pencil,
	Trash,
	UserRoundPlus,
} from "lucide-react";
import { DropdownMenuSeparator } from "../ui/dropdown-menu";
import { CardContent } from "../ui/card";
import Image from "next/image";
import MediaDialog from "../home/media-dialog";
import * as DialogPrimitive from "@radix-ui/react-dialog";
import Comment from "./comment";
import { ScrollArea } from "../ui/scroll-area";
import { Separator } from "../ui/separator";
import { useEffect, useState } from "react";
import { IComment } from "@/types/comment/IComment";

const PostModal = ({
	post,
	setPost,
	visible,
	setVisible,
}: {
	post: IPost;
	setPost: React.Dispatch<React.SetStateAction<IPost>>;
	visible: boolean;
	setVisible: (visible: boolean) => void;
}) => {
	const { user } = useUser();
	const [commentContent, setCommentContent] = useState<string>(``);

	const context = useAppContext();
	const router = useRouter();
	const [comments, setComments] = useState<IComment[]>();

	// handleShare
	const { like, dislike, addComment, getComments } = usePostActions({
		post: post,
		setPost: setPost,
	});

	useEffect(() => {
		const loadComments = async () => {
			if (visible && post?.post_id)
				await getComments(post.post_id, (comments) => setComments(comments));
		};
		loadComments();
		// eslint-disable-next-line react-hooks/exhaustive-deps
	}, [visible, post?.user.user_id, post?.post_id]);

	const handleAddComment = async (e: React.FormEvent) => {
		e.preventDefault();

		if (!commentContent.trim()) return;

		await addComment(
			user?.user_id ?? "",
			commentContent,
			(newComment: IComment) =>
				setComments((prev) => [...(prev as IComment[]), newComment])
		);
	};

	return (
		<Dialog
			open={visible}
			onOpenChange={() => {
				setVisible(false);
				setPost({} as IPost);
			}}
		>
			{visible && (
				<DialogContent className="rounded-4xl  min-w-screen sm:min-w-[900px] max-h-screen text-start bg-background">
					<ScrollArea className="max-h-[84vh] w-full overflow-y-auto">
						<div className=" ">
							<div className="flex justify-between items-center mb-2">
								<DialogTitle className="text-lg font-semibold"></DialogTitle>
							</div>
							<div>
								<MotionCard className=" w-full flex-wrap">
									<div className="">
										<CardHeader className="flex flex-row items-center justify-between px-0 md:px-4">
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
															{post?.user?.username ?? ""}
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
													<DropdownMenuTrigger asChild disabled={false}>
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
											<p className="whitespace-normal break-words mb-4 w-full text-start">
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
																	false
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
																		{/* {loadingImages[media.media_id] && (
																		<div className="absolute inset-0 flex items-center justify-center z-10 bg-black/10">
																			<ImageLoader />
																		</div>
																	)} */}
																		<Image
																			src={
																				false
																					? media.media_src
																					: `http://localhost:5114${media.media_src}`
																			}
																			priority={false}
																			alt={`Media ${media.media_id}`}
																			width={500}
																			height={500}
																			loading="lazy"
																			// onLoad={() => ImageLoad(media.media_id)}
																			// onError={() => ImageError(media.media_id)}
																			quality={1}
																			placeholder="empty"
																			className="object-cover w-full rounded-xl shadow-md"
																		/>
																	</div>
																) : (
																	<video
																		src={`${
																			false
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
								<div className="w-full flex flex-row justify-center gap-2  sm:pt-4 sm:px-10 pt-4 items-center text-center">
									<div className="flex flex-row gap-4 flex-1">
										<MotionButton className="flex-1">
											<Button
												variant={`ghost`}
												size={`icon`}
												className="px-4 rounded-full cursor-pointer"
												disabled={false}
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
													}}
												/>
												{post.like_count > 0 && <p>{post.like_count}</p>}
											</Button>
										</MotionButton>
										<MotionButton className="flex-1">
											<Button
												variant={`ghost`}
												size={`icon`}
												className="px-4 rounded-full cursor-pointer"
												disabled={false}
											>
												<MessageCircle className="w-6 h-6 text-muted-foreground" />
												{post.comment_count > 0 && (
													<p className="text-muted-foreground">
														{post.comment_count}
													</p>
												)}
											</Button>
										</MotionButton>
										<MotionButton className="flex-1">
											<Button
												variant={`ghost`}
												size={`icon`}
												className="p-5 rounded-full cursor-pointer"
												disabled={false}
											>
												<Share className="w-6 h-6 text-muted-foreground" />
											</Button>
										</MotionButton>
										<MotionButton className="flex-1">
											<Button
												variant={`ghost`}
												className="px-4 rounded-full cursor-pointer"
												size={`icon`}
												disabled={false}
											>
												<Bookmark />
											</Button>
										</MotionButton>
									</div>
								</div>
								<Separator className="mt-4 px-4" orientation="horizontal" />
								<div className="w-full flex flex-col gap-2 pt-4 sm:px-10 px-0">
									<h1 className="text-start text-muted-foreground font-semibold text-lg mb-2">
										Comments ({post.comment_count})
									</h1>

									{comments &&
										comments.length > 0 &&
										comments.map((comment) => (
											<Comment
												key={comment.comment_id}
												comment={comment}
												currentUserId={user?.user_id ?? ""}
											/>
										))}
								</div>
							</div>
						</div>
					</ScrollArea>
					<form
						onSubmit={handleAddComment}
						className="outline border-t rounded-4xl flex flex-row gap-2 items-center w-full justify-between sm:p-3  p-2 "
					>
						<input
							type="text"
							value={commentContent}
							onChange={(e) => setCommentContent(e.target.value)}
							placeholder="Write a comment..."
							className="w-full pl-0 sm:p-2 focus:outline-none rounded-4xl hover:outline-none focus:border-0 border-0 "
						/>
						<MotionButton>
							<Button
								className="rounded-full cursor-pointer"
								variant={`secondary`}
								size={`icon`}
							>
								{/* AI */}
								<Sparkles />
							</Button>
						</MotionButton>
						<MotionButton>
							<Button type="submit" className=" rounded-xl cursor-pointer">
								Post
							</Button>
						</MotionButton>
					</form>
					<DialogPrimitive.Close className="ring-offset-background focus:ring-ring data-[state=open]:bg-accent data-[state=open]:text-muted-foreground absolute top-4 right-4 rounded-xs opacity-70 transition-opacity hover:opacity-100 focus:ring-2 focus:ring-offset-2 focus:outline-hidden disabled:pointer-events-none [&_svg]:pointer-events-none [&_svg]:shrink-0 [&_svg:not([class*='size-'])]:size-4">
						<XIcon />
						<span className="sr-only">Close</span>
					</DialogPrimitive.Close>
				</DialogContent>
			)}
		</Dialog>
	);
};

export default PostModal;
// const dummyComments: IComment[] = [
// 	{
// 		comment_id: "cmt_123",
// 		created_at: `1 hour ago`,
// 		content:
// 			"I absolutely love how thoughtful and respectful everyone is in this discussion! It's refreshing to see such a positive exchange of ideas. I can’t wait to see how this evolves. Great job, everyone!",
// 		liked: true,
// 		reply_count: 3,
// 		edited: false,
// 		is_hidden: false,
// 		likes_count: 12,
// 		post_id: "post_123",
// 		user: {
// 			user_id: "user_1",
// 			username: "ivan_petrov",
// 			is_private: false,
// 			pfp_src: "https://api.dicebear.com/7.x/thumbs/svg?seed=Ivan",
// 			notifications: {
// 				friendNotifications: [
// 					{
// 						pfp_src: "https://api.dicebear.com/7.x/thumbs/svg?seed=Ivan",
// 						sender_id: "user_1",
// 						username: "ivan_petrov",
// 						request_sent_at: new Date(),
// 					},
// 				],
// 			},
// 		},
// 		parent_id: "post_123",
// 	},
// 	{
// 		comment_id: "cmt_456",
// 		created_at: `2 hours ago`,
// 		content:
// 			"This is exactly the type of conversation I love being a part of! It's so nice to see everyone treating each other with kindness and open-mindedness. Keep it up, folks!",
// 		liked: true,
// 		reply_count: 4,
// 		edited: false,
// 		is_hidden: false,
// 		likes_count: 8,
// 		post_id: "post_123",
// 		user: {
// 			user_id: "user_2",
// 			username: "maria_ilieva",
// 			is_private: false,
// 			pfp_src: "https://api.dicebear.com/7.x/thumbs/svg?seed=Maria",
// 			notifications: {
// 				friendNotifications: [
// 					{
// 						pfp_src: "https://api.dicebear.com/7.x/thumbs/svg?seed=Maria",
// 						sender_id: "user_2",
// 						username: "maria_ilieva",
// 						request_sent_at: new Date(),
// 					},
// 				],
// 			},
// 		},
// 		parent_id: "post_123",
// 	},
// 	{
// 		comment_id: "cmt_789",
// 		created_at: `3 hours ago`,
// 		content:
// 			"I’m really enjoying this discussion. It’s rare to find a group of people who are so open-minded and thoughtful. Let’s keep the positivity flowing!",
// 		liked: false,
// 		reply_count: 2,
// 		edited: true,
// 		is_hidden: false,
// 		likes_count: 5,
// 		post_id: "post_123",
// 		user: {
// 			user_id: "user_3",
// 			username: "elena_smith",
// 			is_private: false,
// 			pfp_src: "https://api.dicebear.com/7.x/thumbs/svg?seed=Elena",
// 			notifications: {
// 				friendNotifications: [
// 					{
// 						pfp_src: "https://api.dicebear.com/7.x/thumbs/svg?seed=Elena",
// 						sender_id: "user_3",
// 						username: "elena_smith",
// 						request_sent_at: new Date(),
// 					},
// 				],
// 			},
// 		},
// 		parent_id: "post_123",
// 	},
// 	{
// 		comment_id: "cmt_101112",
// 		created_at: `45 minutes ago`,
// 		content:
// 			"This has been such a great discussion! I’m learning so much from everyone’s perspectives. Thanks to all who are contributing so thoughtfully. Let’s keep this going!",
// 		liked: true,
// 		reply_count: 1,
// 		edited: false,
// 		is_hidden: false,
// 		likes_count: 15,
// 		post_id: "post_123",
// 		user: {
// 			user_id: "user_4",
// 			username: "alex_jones",
// 			is_private: false,
// 			pfp_src: "https://api.dicebear.com/7.x/thumbs/svg?seed=Alex",
// 			notifications: {
// 				friendNotifications: [
// 					{
// 						pfp_src: "https://api.dicebear.com/7.x/thumbs/svg?seed=Alex",
// 						sender_id: "user_4",
// 						username: "alex_jones",
// 						request_sent_at: new Date(),
// 					},
// 				],
// 			},
// 		},
// 		parent_id: "post_123",
// 	},
// 	{
// 		comment_id: "cmt_131415",
// 		created_at: `30 minutes ago`,
// 		content:
// 			"I really appreciate how everyone here is taking the time to listen to each other’s points of view. This is exactly the kind of community I want to be a part of. Let’s continue to engage respectfully and thoughtfully!",
// 		liked: true,
// 		reply_count: 6,
// 		edited: false,
// 		is_hidden: false,
// 		likes_count: 20,
// 		post_id: "post_123",
// 		user: {
// 			user_id: "user_5",
// 			username: "julia_martinez",
// 			is_private: false,
// 			pfp_src: "https://api.dicebear.com/7.x/thumbs/svg?seed=Julia",
// 			notifications: {
// 				friendNotifications: [
// 					{
// 						pfp_src: "https://api.dicebear.com/7.x/thumbs/svg?seed=Julia",
// 						sender_id: "user_5",
// 						username: "julia_martinez",
// 						request_sent_at: new Date(),
// 					},
// 				],
// 			},
// 		},
// 		parent_id: "post_123",
// 	},
// ];
