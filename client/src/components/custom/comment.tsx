"use client";

import Link from "next/link";
import { Badge } from "../ui/badge";
import {
	CircleMinus,
	CirclePlus,
	Delete,
	Ellipsis,
	Heart,
	MessageCircle,
	Pencil,
	Reply,
	Trash,
} from "lucide-react";
import { Button } from "../ui/button";
import {
	DropdownMenu,
	DropdownMenuTrigger,
} from "@radix-ui/react-dropdown-menu";
import { DropdownMenuContent, DropdownMenuItem } from "../ui/dropdown-menu";
import { useState } from "react";
import { CustomAvatar } from "./custom-avatar";
import { IBaseUser } from "@/types/IBaseUser";
import { MotionCard } from "../animations/motion-wrapper";
import { formatDistanceToNow } from "date-fns";
import { IComment } from "@/types/comment/IComment";

type CommentProps = {
	comment: IComment;
	currentUserId: IBaseUser[`user_id`];
};

const Comment: React.FC<CommentProps> = ({ comment, currentUserId }) => {
	const [liked, setLiked] = useState(comment.liked);
	const [likes, setLikes] = useState(comment.likes_count);
	const [hidden, setHidden] = useState(false);

	const isOwner = comment.user.user_id === currentUserId;

	const handleLike = () => {
		setLiked(!liked);
		setLikes((prev) => (liked ? prev - 1 : prev + 1));
		// TODO: send like/unlike request to backend
	};

	if (hidden) {
		return (
			<MotionCard className="text-muted-foreground italic px-2 py-1 flex w-full flex-row justify-between items-center">
				<p>This comment has been hidden.</p>
				<Badge
					className="p-2 border outline bg-secondary text-background rounded-full cursor-pointer"
					onClick={() => setHidden(false)}
				>
					<CirclePlus className="w-4 h-4 text-muted-foreground" />
				</Badge>
			</MotionCard>
		);
	}

	return (
		<MotionCard className="flex flex-row gap-2">
			<CustomAvatar
				pfp_src={comment.user.pfp_src}
				size="h-9 w-9"
				username={comment.user.username}
			/>
			<div className="flex flex-col gap-2 w-full">
				<div className="w-full flex px-3 py-2 flex-col gap-1 items-start justify-center rounded-xl">
					<div className="flex flex-row gap-2 items-center justify-start">
						<Link
							href={`/user/${comment.user.username}`}
							className="font-semibold hover:underline items-center cursor-pointer"
						>
							{comment.user.username}
						</Link>
						<p>•</p>
						<p className="text-muted-foreground text-sm">
							{formatDistanceToNow(new Date(comment.created_at), {
								addSuffix: true,
							})}
						</p>
						{comment.edited && (
							<>
								<p>•</p>
								<p className="text-muted-foreground text-sm">Edited</p>
							</>
						)}
					</div>
					<div className="text-muted-foreground text-start">
						{comment.content}
					</div>
				</div>
				<div className="flex flex-row gap-4 px-2 justify-between w-full text-muted-foreground">
					<div className="flex flex-row gap-4 justify-start items-center">
						{/* Like Button */}
						<div className="flex flex-row gap-1 items-center">
							<Badge
								variant="default"
								className="p-2 border outline bg-secondary text-background rounded-full cursor-pointer"
								onClick={handleLike}
							>
								<Heart
									className="w-4 h-4 text-muted-foreground"
									style={{
										fill: liked ? "currentColor" : "none",
									}}
								/>
							</Badge>
							<p>{likes}</p>
						</div>

						{/* Reply Count */}
						<div className="flex flex-row gap-1 items-center">
							<Badge
								variant="default"
								className="p-2 border outline bg-secondary text-background rounded-full cursor-pointer"
							>
								<MessageCircle className="w-4 h-4 text-muted-foreground" />
							</Badge>
							<p>{comment.reply_count}</p>
						</div>

						{/* Reply Action */}
						<div className="flex flex-row gap-1 items-center">
							<Badge
								variant="default"
								className="p-2 border outline bg-secondary text-background rounded-full cursor-pointer"
							>
								<Reply className="w-4 h-4 text-muted-foreground" />
							</Badge>
							<p>Reply</p>
						</div>
					</div>

					{/* Dropdown Menu */}
					<DropdownMenu>
						<DropdownMenuTrigger asChild>
							<Button
								size="icon"
								variant="ghost"
								className="rounded-full cursor-pointer"
							>
								<Ellipsis />
							</Button>
						</DropdownMenuTrigger>
						<DropdownMenuContent className="w-48 px-2 py-4">
							{isOwner ? (
								<>
									<DropdownMenuItem className="p-2 flex justify-between cursor-pointer">
										<p>Edit comment</p>
										<Pencil className="h-4 w-4" />
									</DropdownMenuItem>
									<DropdownMenuItem className="text-red-500 dark:text-red-400 p-2 flex justify-between cursor-pointer">
										<p>Delete comment</p>
										<Trash className="h-4 w-4" />
									</DropdownMenuItem>
								</>
							) : (
								<>
									<DropdownMenuItem
										className="p-2 flex flex-row justify-between cursor-pointer"
										onClick={() => {
											// TODO: hide logic
											setHidden(true);
										}}
									>
										<p>Hide comment</p>
										<CircleMinus className="h-4 w-4" />
									</DropdownMenuItem>
									<DropdownMenuItem className="text-red-500 dark:text-red-400 p-2 flex justify-between cursor-pointer">
										<div className="flex flex-col">
											<p>Report comment</p>
											<p>(coming soon)</p>
										</div>
										<Delete className="h-4 w-4" />
									</DropdownMenuItem>
								</>
							)}
							{/* <DropdownMenuItem
								className="p-2 flex flex-row justify-between cursor-pointer"
								onClick={() => {
									// TODO: hide logic
									setHidden(true);
								}}
							>
								<p>Hide comment</p>
								<CircleMinus className="h-4 w-4" />
							</DropdownMenuItem>

							{isOwner && (
								<>
									<DropdownMenuItem className="p-2 flex justify-between cursor-pointer">
										<p>Edit comment</p>
										<Pencil className="h-4 w-4" />
									</DropdownMenuItem>
									<DropdownMenuItem className="text-red-500 dark:text-red-400 p-2 flex justify-between cursor-pointer">
										<p>Delete comment</p>
										<Trash className="h-4 w-4" />
									</DropdownMenuItem>
								</>
							)}

							<DropdownMenuSeparator className="mt-2 mb-2" />
							<DropdownMenuItem className="text-red-500 dark:text-red-400 p-2 flex justify-between cursor-pointer">
								<p>Report comment</p>
								<Delete className="h-4 w-4" />
							</DropdownMenuItem> */}
						</DropdownMenuContent>
					</DropdownMenu>
				</div>
			</div>
		</MotionCard>
	);
};

export default Comment;
