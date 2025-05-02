"use client";
import { ScanEye, Send, Upload, X } from "lucide-react";
import { Button } from "../ui/button";
import { Textarea } from "../ui/textarea";
import { Avatar, AvatarFallback, AvatarImage } from "../ui/avatar";
import { useEffect } from "react";
import { Dialog, DialogTrigger, DialogTitle } from "../ui/dialog";
import { DialogContent } from "@radix-ui/react-dialog";
import Post from "../custom/post";
import { useUser } from "@/contexts/UserContext";
import { PostActionTypeEnum as ActionType } from "@/enums/ICreatePostActionSetType";
import useCreatePost from "@/hooks/useCreatePost";
import { IMediaPreview } from "@/types/IMediaPreview";

const CreatePost = () => {
	const {
		state,
		dispatch,
		handleFileInput,
		handleSubmit,
		fileInputRef,
		textareaRef,
	} = useCreatePost();
	const user = useUser();

	useEffect(() => {
		if (state.isExpanded && textareaRef.current) textareaRef.current.focus();
	}, [state.isExpanded, textareaRef]);

	return (
		<div
			onDragOver={(e) => {
				e.preventDefault();
				dispatch({ type: ActionType.SET_DRAG, payload: true });
			}}
			onDrop={(e) => {
				e.preventDefault();
				dispatch({ type: ActionType.SET_DRAG, payload: false });
				if (e.dataTransfer.files.length) handleFileInput(e.dataTransfer.files);
			}}
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
						value={state.formData.content}
						disabled={state.isLoading}
						ref={textareaRef}
						onChange={(e) =>
							dispatch({
								type: ActionType.SET_CONTENT,
								payload: e.target.value,
							})
						}
						onFocus={() =>
							dispatch({ type: ActionType.SET_EXPANDED, payload: true })
						}
					/>
				</div>
				<div className="w-full px-4">
					{state.isExpanded && (
						<div className="mt-4 border-2 border-dashed border-blue-400 dark:border-blue-500 rounded-lg p-8 text-center cursor-pointer">
							{state.formData.files.length > 0 ? (
								<div className="grid grid-cols-2 gap-4">
									{state.formData.files.map(
										(media: IMediaPreview, index: number) => (
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
														dispatch({
															type: ActionType.SET_FILES,
															payload: state.formData.files.filter(
																(_: IMediaPreview, i: number) => i !== index
															),
														});
													}}
												>
													<X />
												</Button>
											</div>
										)
									)}
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
										disabled={state.isLoading}
										multiple
										onChange={(e) => {
											if (e.target.files) {
												handleFileInput(e.target.files);
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
						<Button
							type="button"
							disabled={state.isLoading}
							onClick={() => fileInputRef.current?.click()}
							className="no-underline flex items-center gap-1 sm:gap-2 cursor-pointer rounded-full p-4 outline shadow-md"
							variant={`link`}
						>
							<Upload className="w-5 h-5" />
							<span>Add Media</span>
						</Button>
					</div>
					<div className="flex flex-row gap-2">
						{state.isExpanded && (
							<div className="flex flex-row gap-2">
								<Button
									variant={`secondary`}
									disabled={state.isLoading}
									className="cursor-pointer rounded-full p-4 outline shadow-md"
									onClick={() => {
										dispatch({ type: ActionType.RESET_FORM });
										dispatch({ type: ActionType.SET_EXPANDED, payload: false });
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
												state.formData.content.trim() === "" &&
												state.formData.files.length === 0
											}
											onClick={() => {
												console.log(`View media files`, state.formData.files);
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
														content: state.formData.content,
														media: state.formData.files.map(
															(media: IMediaPreview) => ({
																type: media.type,
																url: media.url,
															})
														),
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
							disabled={state.isLoading}
							className="cursor-pointer rounded-full p-4 outline shadow-md"
						>
							<Send />
							Post
						</Button>
					</div>
				</div>
			</form>
		</div>
	);
};

export default CreatePost;
