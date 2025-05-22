import { step } from "@/types/stepType";
import { motion } from "framer-motion";
import { Label } from "../ui/label";
import { Globe, MapPin, User } from "lucide-react";
import { Input } from "../ui/input";
import { IRegisterErrors } from "@/types/IRegisterErrors";
import { IRegisterForm } from "@/types/form/IRegisterForm";
import { Textarea } from "../ui/textarea";

const ProfileStep = ({
	fullNameRef,
	errors,
	formData,
	setFormData,
	isLoading,
}: {
	formData: IRegisterForm;
	setFormData: React.Dispatch<React.SetStateAction<IRegisterForm>>;
	fullNameRef: React.RefObject<HTMLInputElement>;
	errors: IRegisterErrors;
	isLoading: boolean;
}) => {
	return (
		<motion.div
			key={`profile` as step}
			initial={{ opacity: 0, x: 20 }}
			animate={{ opacity: 1, x: 0 }}
			exit={{ opacity: 0, x: -20 }}
			transition={{ duration: 0.3 }}
			className="space-y-4"
		>
			{/* Title */}
			<div className="text-center mb-4">
				<h2 className="text-xl font-bold">Profile Information</h2>
				<p className="text-sm text-gray-500">Tell us about yourself</p>
			</div>

			{/* FullName */}
			<div className="space-y-2">
				<Label htmlFor="fullName">Full Name</Label>
				<div className="relative">
					<User className="absolute left-3 top-2 h-4 w-4 text-gray-400 " />
					<Input
						id="fullName"
						name="fullName"
						placeholder="John Doe"
                        required
						tabIndex={1}
						ref={fullNameRef}
						className={`pl-10 border outline shadow-md rounded-lg bg-secondary ${
							errors.fullName ? `border-red-500 focus-visible:ring-red-500` : ``
						}`}
						value={formData.fullName}
						onChange={(e) =>
							setFormData({ ...formData, fullName: e.target.value })
						}
						disabled={isLoading}
					/>
				</div>
				{errors.fullName && (
					<motion.p
						initial={{ opacity: 0, y: -10 }}
						animate={{ opacity: 1, y: 0 }}
						className="text-sm text-red-500"
					>
						{errors.fullName}
					</motion.p>
				)}
			</div>

			{/* Bio */}
			<div className="space-y-2">
				<Label htmlFor="bio">Bio</Label>
				<Textarea
					id="bio"
					name="bio"
					placeholder="Tell us about yourself"
                    required
					tabIndex={2}
					className={`pl-10 border outline shadow-md rounded-lg bg-secondary ${
						errors.bio ? `border-red-500 focus-visible:ring-red-500` : ``
					}`}
					value={formData.bio}
					onChange={(e) => setFormData({ ...formData, bio: e.target.value })}
					disabled={isLoading}
					rows={3}
				/>
				{errors.bio && (
					<motion.p
						initial={{ opacity: 0, y: -10 }}
						animate={{ opacity: 1, y: 0 }}
						className="text-sm text-red-500"
					>
						{errors.bio}
					</motion.p>
				)}
			</div>

			{/* Location */}
			<div className="space-y-2">
				<Label htmlFor="location">Location</Label>
				<div className="relative">
					<MapPin className="absolute left-3 top-2 h-4 w-4 text-gray-400 " />
					<Input
						id="location"
						name="location"
						placeholder="Eindhoven, Netherlands"
                        required
						tabIndex={1}
						className={`pl-10 border outline shadow-md rounded-lg bg-secondary ${
							errors.location ? `border-red-500 focus-visible:ring-red-500` : ``
						}`}
						value={formData.location}
						onChange={(e) =>
							setFormData({ ...formData, location: e.target.value })
						}
						disabled={isLoading}
					/>
				</div>
				{errors.location && (
					<motion.p
						initial={{ opacity: 0, y: -10 }}
						animate={{ opacity: 1, y: 0 }}
						className="text-sm text-red-500"
					>
						{errors.location}
					</motion.p>
				)}
			</div>

			{/* Website */}
			<div className="space-y-2">
				<Label htmlFor="website">Website</Label>
				<div className="relative">
					<Globe className="absolute left-3 top-2 h-4 w-4 text-gray-400 " />
					<Input
						id="website"
						name="website"
						placeholder="https://crackflix.site"
                        required
						tabIndex={1}
						className={`pl-10 border outline shadow-md rounded-lg bg-secondary ${
							errors.website ? `border-red-500 focus-visible:ring-red-500` : ``
						}`}
						value={formData.website}
						onChange={(e) =>
							setFormData({ ...formData, website: e.target.value })
						}
						disabled={isLoading}
					/>
				</div>
				{errors.website && (
					<motion.p
						initial={{ opacity: 0, y: -10 }}
						animate={{ opacity: 1, y: 0 }}
						className="text-sm text-red-500"
					>
						{errors.website}
					</motion.p>
				)}
			</div>
		</motion.div>
	);
};

export default ProfileStep;
