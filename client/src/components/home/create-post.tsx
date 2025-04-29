"use client";
import { ScanEye, Send, Upload, X } from "lucide-react";
import { Button } from "../ui/button";
import { Textarea } from "../ui/textarea";
import { Avatar, AvatarFallback, AvatarImage } from "../ui/avatar";
import { useEffect, useRef, useState } from "react";
import { Dialog, DialogTrigger, DialogTitle } from "../ui/dialog";
import { DialogContent } from "@radix-ui/react-dialog";
import Post from "../custom/post";
import { useUser } from "@/contexts/UserContext";
import { Toaster } from "../ui/sonner";
import createPost from "@/api/post/create-post";
import {
	// ConvPostFormData,
	CreatePostFormData,
} from "@/types/ICreatePostFormData";
import UseTokenHook from "@/hooks/useTokenHook";

const CreatePost = () => {
	const [isDrag, setDrag] = useState(false);
	const [formData, setFormData] = useState<CreatePostFormData>({
		content: "",
		files: [],
	});
	const [isExpanded, setIsExpanded] = useState(false);
	const fileInputRef = useRef<HTMLInputElement>(null);
	const textareaRef = useRef<HTMLTextAreaElement>(null);
	const user = useUser();
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
		const newFiles = Array.from(files).map(
			(file) =>
				({
					type: file.type.startsWith("image/") ? "image" : "video",
					url: URL.createObjectURL(file),
					file,
				} as const)
		);

		setFormData((prev) => ({
			...prev,
			files: [...prev.files, ...newFiles], // Changed from mediaFiles to files
		}));
	};

	const handleSubmit = async (e: React.FormEvent) => {
		e.preventDefault();

		if (formData.content.trim() === "" && formData.files.length === 0) return;

		console.log("Content being sent:", formData.content);

		const token = await UseTokenHook();

		// Ensure Content-Type is not manually set in the FormData submission
		const convFormData = new FormData();
		convFormData.append("content", formData.content);
		formData.files.forEach((media) => {
			convFormData.append("files", media.file);
		});

		console.log(
			`convFormData`,
			convFormData.get(`content`),
			convFormData.getAll(`files`)
		);

		const res = await createPost(token, convFormData); // convFormData
		if (res.status === 200) {
			console.log(`Post created successfully`);
		}
		console.log(`res`, res);

		setFormData({ content: "", files: [] });
		setIsExpanded(false);
	};

	return (
		<div
			onDragOver={handleDragOver}
			onDragLeave={handleDragLeave}
			onDrop={handleDrop}
		>
			<form onSubmit={handleSubmit} className="py-4 flex flex-col gap-2">
				<div className="flex flex-row gap-2 w-full px-4 pt-2.5">
					{/* Avatar */}
					<Avatar className="w-9 h-9">
						<AvatarImage src={user?.pfp_src ?? ""} alt="User pic" />
						<AvatarFallback>{user?.username.charAt(0)}</AvatarFallback>
					</Avatar>
					<Textarea
						placeholder="What's on your mind?"
						className="w-full h-15 outline border shadow-md resize-none"
						value={formData.content}
						ref={textareaRef}
						onChange={(e) =>
							setFormData((prev) => ({ ...prev, content: e.target.value }))
						}
						onFocus={() => setIsExpanded(true)}
					/>
				</div>
				<div className="w-full px-4">
					{isExpanded && (
						<div className="mt-4 border-2 border-dashed border-blue-400 dark:border-blue-500 rounded-lg p-8 text-center cursor-pointer">
							{formData.files.length > 0 ? (
								<div className="grid grid-cols-2 gap-4">
									{formData.files.map((media, index) => (
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
													setFormData((prev) => ({
														...prev,
														files: prev.files.filter((_, i) => i !== index), // Changed from mediaFiles to files
													}));
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
										setFormData({ content: "", files: [] });
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
											disabled={
												formData.content.trim() === "" &&
												formData.files.length === 0
											}
											onClick={() => {
												console.log(`View media files`, formData.files);
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
															user_id: user?.user_id,
															username: user?.username,
															pfp_src: user?.pfp_src,
														},
														content: formData.content,
														media: formData.files.map((media) => ({
															type: media.type,
															url: media.url,
														})), // Changed from mediaFiles to files
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
							type="submit"
							className="cursor-pointer rounded-full p-4 outline shadow-md"
						>
							<Send />
							Post
						</Button>
					</div>
				</div>
			</form>
			<Toaster />
		</div>
	);
};

export default CreatePost;
