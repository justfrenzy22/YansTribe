"use client";
import { Cross, ScanEye, Send, Upload, X } from "lucide-react";
import { Button } from "../ui/button";
import { Textarea } from "../ui/textarea";
import { Avatar, AvatarFallback, AvatarImage } from "../ui/avatar";
import { useEffect, useRef, useState } from "react";
import { Dialog, DialogTrigger, DialogTitle } from "../ui/dialog";
import { DialogContent } from "@radix-ui/react-dialog";
import Post from "../custom/post";

type MediaPreview = {
	type: "image" | "video";
	url: string;
	file: File;
};

const CreatePost = () => {
	const [isDrag, setDrag] = useState(false);
	const [content, setContent] = useState("");
	const [mediaFiles, setMediaFiles] = useState<MediaPreview[]>([]);
	const [isExpanded, setIsExpanded] = useState(false);
	const fileInputRef = useRef<HTMLInputElement>(null);
	const textareaRef = useRef<HTMLTextAreaElement>(null);
	// const { username } = useContext<UserContextValue>(UserContext);

	useEffect(() => {
		if (isExpanded && textareaRef.current) {
			textareaRef.current.focus();
		}
	}, [isExpanded]);

	const handleDragOver = (e: React.DragEvent) => {
		e.preventDefault();
		setDrag(true);
	};

	const handleDragLeave = () => {
		setDrag(false);
	};

	const handleDrop = (e: React.DragEvent) => {
		e.preventDefault();
		setDrag(false);

		if (e.dataTransfer.files && e.dataTransfer.files.length > 0) {
			addFiles(e.dataTransfer.files);
		}
	};

	const addFiles = (files: FileList) => {
		const newMediaFiles = Array.from(files).map((file) => ({
			type: file.type.startsWith("image/")
				? ("image" as "image")
				: ("video" as "video"),
			url: URL.createObjectURL(file),
			file,
		}));

		setMediaFiles([...mediaFiles, ...newMediaFiles]);
	};

	return (
		<div
			className="py-4 flex flex-col gap-2"
			onDragOver={handleDragOver}
			onDragLeave={handleDragLeave}
			onDrop={handleDrop}
		>
			<div className="flex flex-row gap-2 w-full px-4 pt-2.5">
				{/* Avatar */}
				<Avatar className="w-9 h-9">
					<AvatarImage src={`asdadiji`} alt="User pic" />
					<AvatarFallback>YT</AvatarFallback>
				</Avatar>
				<Textarea
					placeholder="What's on your mind?"
					className="w-full h-15 outline border shadow-md resize-none"
					value={content}
					ref={textareaRef}
					onChange={(e) => setContent(e.target.value)}
					onFocus={() => setIsExpanded(true)}
				/>
			</div>
			<div className="w-full px-4">
				{isExpanded && (
					<div className="mt-4 border-2 border-dashed border-blue-400 dark:border-blue-500 rounded-lg p-8 text-center cursor-pointer">
						{mediaFiles.length > 0 ? (
							<div className="grid grid-cols-2 gap-4">
								{mediaFiles.map((media, index) => (
									<div key={index} className="relative">
										{media.type === "image" ? (
											<img
												src={media.url}
												alt={`Uploaded media ${index + 1}`}
												className="w-full h-auto rounded-lg"
											/>
										) : (
											<video
												src={media.url}
												controls
												className="w-full h-auto rounded-lg"
											/>
										)}
										<Button
											size={`icon`}
											variant={`destructive`}
											className="absolute top-2 right-2 rounded-full p-1 cursor-pointer"
											onClick={() => {
												setMediaFiles((prev) =>
													prev.filter((_, i) => i !== index)
												);
											}}
										>
											<X />
										</Button>
									</div>
								))}
							</div>
						) : (
							<>
								<p
									className="text-blue-500 dark:text-blue-400"
									onClick={() => fileInputRef.current?.click()}
								>
									Drop or add media files here
								</p>
								<input
									type="file"
									ref={fileInputRef}
									className="hidden"
									multiple
									onChange={(e) => {
										if (e.target.files) {
											addFiles(e.target.files);
											e.target.value = "";
										}
									}}
								/>
							</>
						)}
					</div>
				)}
			</div>
			<div className="flex flex-row sm:justify-between justify-start items-center px-0 sm:px-6">
				<div className="flex items-center gap-0 sm:gap-2">
					{/* Custom button to trigger file input */}
					<Button
						type="button"
						onClick={() => fileInputRef.current?.click()} // Ensure this triggers the file input
						className="no-underline flex items-center gap-1 sm:gap-2 cursor-pointer rounded-full p-4 outline shadow-md"
						variant={`link`}
					>
						<Upload className="w-5 h-5" />
						<span>Add Media</span>
					</Button>
				</div>
				<div className="flex flex-row gap-2">
					{isExpanded && (
						<div className="flex flex-row gap-2">
							<Button
								variant={`secondary`}
								className="cursor-pointer rounded-full p-4 outline shadow-md"
								onClick={() => {
									setContent("");
									setMediaFiles([]);
									setIsExpanded(false);
								}}
							>
								Cancel
							</Button>
							<Dialog>
								<DialogTrigger asChild>
									<Button
										variant={`secondary`}
										className="cursor-pointer rounded-full p-4 outline shadow-md"
										disabled={content.trim() === "" && mediaFiles.length === 0}
										onClick={() => {
											console.log(`View media files`, mediaFiles);
										}}
									>
										<ScanEye />
										View
									</Button>
								</DialogTrigger>
								<DialogContent className="fixed inset-0 flex items-center justify-center backdrop-blur-sm bg-transparent bg-opacity-50 z-50 md:space-y-4 space-y-0  ">
									<div className="max-w-screen  mx-auto rounded-lg shadow-lg p-4 max-h-[90vh] bg-background overflow-y-auto">
										<div className="rounded-lg">
											<div className="flex justify-between items-center mb-2">
												<DialogTitle className="text-lg font-semibold">
													Post Preview
												</DialogTitle>
												<Button
													variant="ghost"
													size="icon"
													className="rounded-full cursor-pointer"
													onClick={() => {
														document.dispatchEvent(
															new KeyboardEvent("keydown", { key: "Escape" })
														);
													}}
												>
													<X className="w-5 h-5" />
												</Button>
											</div>
											<Post
												post={{
													post_id: 1,
													user: {
														user_id: 1,
														username: "JohnDoe",
														pfp_src: "/images/user1.jpg",
													},
													content,
													media: mediaFiles.map((media) => ({
														type: media.type,
														url: media.url,
													})),
													likes: 1,
													comments: 1,
													created_at: new Date().toISOString(),
												}}
												isViewMode={true}
											/>
										</div>
									</div>
								</DialogContent>
							</Dialog>
						</div>
					)}
					<Button
						variant={`default`}
						className="cursor-pointer rounded-full p-4 outline shadow-md"
					>
						<Send />
						Post
					</Button>
				</div>
			</div>
		</div>
	);
};

export default CreatePost;
