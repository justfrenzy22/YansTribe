import { IRegisterForm } from "@/types/IRegisterForm";
import { motion } from "framer-motion";
import { Label } from "../ui/label";
import Link from "next/link";
import { IRegisterErrors } from "@/types/IRegisterErrors";
import { Checkbox } from "../ui/checkbox";
import { Button } from "../ui/button";
import { Github, Linkedin, Twitter } from "lucide-react";

const ReviewStep = ({
	formData,
	setFormData,
	errors,
	isLoading,
}: {
	formData: IRegisterForm;
	setFormData: React.Dispatch<React.SetStateAction<IRegisterForm>>;
	errors: IRegisterErrors;
	isLoading: boolean;
}) => {
	return (
		<motion.div
			key="review"
			initial={{ opacity: 0, x: 20 }}
			animate={{ opacity: 1, x: 0 }}
			exit={{ opacity: 0, x: -20 }}
			transition={{ duration: 0.3 }}
			className="space-y-4"
		>
			<div className="text-center mb-4">
				<h2 className="text-xl font-bold">Review & Confirm</h2>
				<p className="text-sm text-gray-500 dark:text-gray-400">
					Please review your information
				</p>
			</div>

			<div className="space-y-4 bg-secondary p-4 border outline shadow-md rounded-lg ">
				<div className="grid grid-cols-2 gap-4">
					<div>
						<h3 className="text-sm font-medium text-gray-500 dark:text-gray-400">
							Username
						</h3>
						<p>{formData.username || "Not specified"}</p>
					</div>
					<div>
						<h3 className="text-sm font-medium text-gray-500 dark:text-gray-400">
							Email
						</h3>
						<p>{formData.email || "Not specified"}</p>
					</div>
					<div>
						<h3 className="text-sm font-medium text-gray-500 dark:text-gray-400">
							Full Name
						</h3>
						<p>{formData.fullName || "Not specified"}</p>
					</div>
					<div>
						<h3 className="text-sm font-medium text-gray-500 dark:text-gray-400">
							Location
						</h3>
						<p>{formData.location || "Not specified"}</p>
					</div>
					<div className="col-span-2">
						<h3 className="text-sm font-medium text-gray-500 dark:text-gray-400">
							Bio
						</h3>
						<p>{formData.bio || "Not specified"}</p>
					</div>
					<div className="col-span-2">
						<h3 className="text-sm font-medium text-gray-500 dark:text-gray-400">
							Website
						</h3>
						<p>{formData.website || "Not specified"}</p>
					</div>
				</div>
			</div>

			<div className="flex items-start space-x-2">
				<Checkbox
					id="terms"
					className={
						errors.agreeTerms
							? "border-red-500 data-[state=checked]:bg-red-500"
							: ""
					}
					checked={formData.agreeTerms}
					required
					onCheckedChange={(e) =>
						setFormData({ ...formData, agreeTerms: e as boolean })
					}
					disabled={isLoading}
				/>
				<div className="grid gap-1.5 leading-none">
					<Label htmlFor="terms" className="text-sm font-normal">
						I agree to the{" "}
						<Link
							href="/terms"
							className="text-blue-600 dark:text-blue-500 hover:underline"
							onClick={(e) => e.preventDefault()}
						>
							Terms of Service
						</Link>{" "}
						and{" "}
						<Link
							href="/privacy"
							className="text-blue-600 dark:text-blue-500 hover:underline"
							onClick={(e) => e.preventDefault()}
						>
							Privacy Policy
						</Link>
					</Label>
					{errors.agreeTerms && (
						<motion.p
							initial={{ opacity: 0, y: -10 }}
							animate={{ opacity: 1, y: 0 }}
							className="text-sm text-red-500"
						>
							{errors.agreeTerms}
						</motion.p>
					)}
				</div>
			</div>

			<div className="relative my-6">
				<div className="absolute inset-0 flex items-center">
					<div className="w-full border-t border-gray-200 dark:border-gray-800"></div>
				</div>
				<div className="relative flex justify-center text-xs uppercase">
					<span className="bg-secondary px-2 rounded-full ">Comming Soon</span>
				</div>
			</div>

			<div className="grid grid-cols-3 gap-2.5">
				<motion.div
					whileHover={{ scale: 1.02 }}
					whileTap={{ scale: 0.98 }}
					className="mb-2"
				>
					<Button
						variant="outline"
						type="button"
						className="w-full cursor-pointer outline border shadow-md rounded-lg"
						disabled={isLoading}
					>
						<Github className="mr-2 h-4 w-4" />
						Github
					</Button>
				</motion.div>
				<motion.div
					whileHover={{ scale: 1.02 }}
					whileTap={{ scale: 0.98 }}
					className="mb-2"
				>
					<Button
						variant="outline"
						type="button"
						className="w-full cursor-pointer outline border shadow-md rounded-lg"
						disabled={isLoading}
					>
						<Twitter className="mr-2 h-4 w-4" />
						Twitter
					</Button>
				</motion.div>
				<motion.div
					whileHover={{ scale: 1.02 }}
					whileTap={{ scale: 0.98 }}
					className="mb-2"
				>
					<Button
						variant="outline"
						type="button"
						className="w-full cursor-pointer outline border shadow-md rounded-lg"
						disabled={isLoading}
					>
						<Linkedin className="mr-2 h-4 w-4" />
						LinkedIn
					</Button>
				</motion.div>
			</div>
		</motion.div>
	);
};

export default ReviewStep;
