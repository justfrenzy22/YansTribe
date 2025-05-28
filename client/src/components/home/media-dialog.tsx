"use client";

import { useState } from "react";
import {
	Dialog,
	DialogTrigger,
	DialogContent,
	DialogTitle,
} from "../ui/dialog";
import Image from "next/image";
import ImageLoader from "../loader/ImageLoader";

interface MediaDialogProps {
	src: string;
	alt: string;
	media_type: "image" | "video";
	children: React.ReactNode;
}

function MediaDialog({ src, alt, media_type, children }: MediaDialogProps) {
	const [isLoading, setLoading] = useState<boolean>(true);

	const handleImageLoad = () => {
		setLoading(false);
	};

	const handleDialogChange = (open: boolean) => {
		if (open) {
			setLoading(true);
		}
	};

	return (
		<Dialog onOpenChange={handleDialogChange}>
			<DialogTrigger asChild>{children}</DialogTrigger>

			<DialogContent className="p-4 rounded-lg shadow-lg backdrop-blur-sm bg-transparent bg-opacity-50 items-center justify-center h-[95vh] sm:max-w-[95vw] max-w-[95vw]">
				<DialogTitle className="sr-only">Extended Media: {alt}</DialogTitle>

				{media_type === `image` ? (
					<>
						{isLoading && (
							<div className="absolute inset-0 flex items-center justify-center">
								<ImageLoader />
							</div>
						)}
						<Image
							src={src}
							alt={alt}
							fill={true}
							quality={100}
							className={`object-contain max-w-[90vw] max-h-[90vh] transition-opacity duration-300 ease-in-out
                        ${isLoading ? "opacity-0" : "opacity-100"}
                    `}
							onLoad={handleImageLoad}
							onError={() => {
								setLoading(false);
								console.error(`Failed to load image: ${src}`);
							}}
						/>
					</>
				) : (
					<video
						src={src}
						controls
						className={`object-contain max-w-[90vw] max-h-[90vh] transition-opacity duration-300 ease-in-out`}
					/>
				)}
			</DialogContent>
		</Dialog>
	);
}

export default MediaDialog;
