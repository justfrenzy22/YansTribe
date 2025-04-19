"use client";

import Link from "next/link";
import { Avatar, AvatarFallback, AvatarImage } from "@/components/ui/avatar";
import { Card } from "@/components/ui/card";
import { Badge } from "../ui/badge";
import {
	Bookmark,
	CircleMinus,
	CirclePlus,
	Delete,
	Ellipsis,
	Heart,
	MessageCircle,
	Pencil,
	Reply,
	Trash,
	UserRoundPlus,
} from "lucide-react";
import { Button } from "../ui/button";
import {
	DropdownMenu,
	DropdownMenuTrigger,
} from "@radix-ui/react-dropdown-menu";
import {
	DropdownMenuContent,
	DropdownMenuItem,
	DropdownMenuLabel,
	DropdownMenuSeparator,
} from "../ui/dropdown-menu";
import { IComment } from "@/types/IComment";
import { useState } from "react";

type CommentProps = {
	comment: IComment;
	currentUserId: string;
};

const Comment: React.FC<CommentProps> = ({ comment, currentUserId }) => {
	const [liked, setLiked] = useState(comment.liked);
	const [likes, setLikes] = useState(comment.liked_count);
	const [hidden, setHidden] = useState(false);

	const isOwner = comment.user_id === currentUserId;

	const handleLike = () => {
		setLiked(!liked);
		setLikes((prev) => (liked ? prev - 1 : prev + 1));
		// TODO: send like/unlike request to backend
	};

	if (hidden) {
		return (
			<div className="text-muted-foreground italic px-2 py-1 flex w-full flex-row justify-between items-center">
				<p>This comment has been hidden.</p>
				<Badge
					className="p-2 border outline bg-secondary text-background rounded-full cursor-pointer"
					onClick={() => setHidden(false)}
				>
					<CirclePlus className="w-4 h-4 text-muted-foreground" />
				</Badge>
			</div>
		);
	}

	return (
		<div className="flex flex-row gap-2">
			<Avatar className="w-9 h-9">
				<AvatarImage src={comment.pfp_src} alt="User pic" />
				<AvatarFallback>
					{comment.username.slice(0, 2).toUpperCase()}
				</AvatarFallback>
			</Avatar>
			<div className="flex flex-col gap-2 w-full">
				<div className="w-full flex px-3 py-2 flex-col gap-1 items-start justify-center rounded-xl">
					<div className="flex flex-row gap-2 items-center justify-start">
						<Link
							href={`/user/${comment.username}`}
							className="font-semibold hover:underline items-center cursor-pointer"
						>
							{comment.username}
						</Link>
						<p>•</p>
						<p className="text-muted-foreground text-sm">
							{comment.created_at}
						</p>
					</div>
					<div className="text-muted-foreground">{comment.content}</div>
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
										<p>Report comment</p>
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
		</div>
	);
};

export default Comment;
