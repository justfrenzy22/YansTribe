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
import * as DialogPrimitive from "@radix-ui/react-dialog";
import { XIcon } from "lucide-react";

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
				<DialogPrimitive.Close className="ring-offset-background focus:ring-ring data-[state=open]:bg-accent data-[state=open]:text-muted-foreground absolute top-2 right-2 rounded-xs opacity-70 transition-opacity hover:opacity-100 focus:ring-2 focus:ring-offset-2 focus:outline-hidden disabled:pointer-events-none [&_svg]:pointer-events-none [&_svg]:shrink-0 [&_svg:not([class*='size-'])]:size-4">
					<XIcon />
				</DialogPrimitive.Close>
			</DialogContent>
		</Dialog>
	);
}

export default MediaDialog;
