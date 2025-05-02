import { motion } from "framer-motion";
import { Label } from "../ui/label";
import { Eye, EyeOff, Lock, Mail, User } from "lucide-react";
import { Input } from "../ui/input";
import { IRegisterForm } from "@/types/IRegisterForm";
import { IRegisterErrors } from "@/types/IRegisterErrors";
import { Button } from "../ui/button";
import { step } from "@/types/stepType";

const AccountStep = ({
	errors,
	formData,
	setFormData,
	isLoading,
	usernameRef,
	showPassword,
	showConfirmPassword,
	setShowPassword,
	setShowConfirmPassword,
}: {
	formData: IRegisterForm;
	setFormData: React.Dispatch<React.SetStateAction<IRegisterForm>>;
	errors: IRegisterErrors;
	isLoading: boolean;
	usernameRef: React.RefObject<HTMLInputElement>;
	showPassword: boolean;
	showConfirmPassword: boolean;
	setShowPassword: React.Dispatch<React.SetStateAction<boolean>>;
	setShowConfirmPassword: React.Dispatch<React.SetStateAction<boolean>>;
}) => {
	return (
		<motion.div
			key={`account` as step}
			initial={{ opacity: 0, x: 20 }}
			animate={{ opacity: 1, x: 0 }}
			exit={{ opacity: 0, x: -20 }}
			transition={{ duration: 0.3 }}
			className="space-y-4"
		>
			{/* Title */}
			<div className="text-center mb-4">
				<h2 className="text-xl font-bold">Account Information</h2>
				<p className="text-sm text-gray-500 dark:text-gray-400">
					Create your login credentials
				</p>
			</div>

			{/* Username */}
			<div className="space-y-2">
				<Label htmlFor="username">Username</Label>
				<div className="relative">
					<User className="absolute left-3 top-3 h-4 w-4 text-gray-400 " />
					<Input
						id="username"
						name="username"
						placeholder="johndoe"
						required
						tabIndex={1}
						ref={usernameRef}
						className={`pl-10 border outline shadow-md rounded-lg bg-secondary ${
							errors.username ? `border-red-500 focus-visible:ring-red-500` : ``
						}`}
						value={formData.username}
						onChange={(e) =>
							setFormData({ ...formData, username: e.target.value })
						}
						disabled={isLoading}
					/>
				</div>
				{errors.username && (
					<motion.p
						initial={{ opacity: 0, y: -10 }}
						animate={{ opacity: 1, y: 0 }}
						className="text-sm text-red-500"
					>
						{errors.username}
					</motion.p>
				)}
			</div>

			{/* Email */}
			<div className="space-y-2">
				<Label htmlFor="email">Email</Label>
				<div className="relative">
					<Mail className="absolute left-3 top-3 h-4 w-4 text-gray-400 " />
					<Input
						id="email"
						name="email"
						placeholder="name@example.com"
						required
						tabIndex={1}
						className={`pl-10 border outline shadow-md rounded-lg bg-secondary ${
							errors.email ? `border-red-500 focus-visible:ring-red-500` : ``
						}`}
						value={formData.email}
						onChange={(e) =>
							setFormData({ ...formData, email: e.target.value })
						}
						disabled={isLoading}
					/>
				</div>
				{errors.email && (
					<motion.p
						initial={{ opacity: 0, y: -10 }}
						animate={{ opacity: 1, y: 0 }}
						className="text-sm text-red-500"
					>
						{errors.email}
					</motion.p>
				)}
			</div>

			{/* Password */}
			<div className="space-y-2">
				<Label htmlFor="password">Password</Label>
				<div className="relative">
					<Lock className="absolute left-3 top-3 h-4 w-4 text-gray-400 " />
					<Input
						id="password"
						name="password"
						placeholder="password"
						required
						type={showPassword ? `text` : `password`}
						tabIndex={1}
						className={`pl-10 border outline shadow-md rounded-lg bg-secondary ${
							errors.password ? `border-red-500 focus-visible:ring-red-500` : ``
						}`}
						value={formData.password}
						onChange={(e) =>
							setFormData({ ...formData, password: e.target.value })
						}
						disabled={isLoading}
					/>
					<Button
						type="button"
						onClick={() => setShowPassword(!showPassword)}
						className="absolute right-2 bottom-0 cursor-pointer rounded-full p-2 border-none bg-transparent hover:bg-transparent"
						variant={`ghost`}
					>
						{showPassword ? <Eye /> : <EyeOff />}
					</Button>
				</div>
				{errors.password && (
					<motion.p
						initial={{ opacity: 0, y: -10 }}
						animate={{ opacity: 1, y: 0 }}
						className="text-sm text-red-500"
					>
						{errors.password}
					</motion.p>
				)}
			</div>

			{/* Confirm Password */}
			<div className="space-y-2">
				<Label htmlFor="confirmPassword">Confirm Password</Label>
				<div className="relative">
					<Lock className="absolute left-3 top-3 h-4 w-4 text-gray-400 " />
					<Input
						id="confirmPassword"
						name="confirmPassword"
						placeholder="password"
						required
						type={showConfirmPassword ? `text` : `password`}
						tabIndex={1}
						className={`pl-10 border outline shadow-md rounded-lg bg-secondary ${
							errors.confirmPassword
								? `border-red-500 focus-visible:ring-red-500`
								: ``
						}`}
						value={formData.confirmPassword}
						onChange={(e) =>
							setFormData({
								...formData,
								confirmPassword: e.target.value,
							})
						}
						disabled={isLoading}
					/>
					<Button
						type="button"
						onClick={() => setShowConfirmPassword(!showConfirmPassword)}
						className="absolute right-2 bottom-0 cursor-pointer rounded-full p-2 border-none bg-transparent hover:bg-transparent"
						variant={`ghost`}
					>
						{showConfirmPassword ? <Eye /> : <EyeOff />}
					</Button>
				</div>
				{errors.confirmPassword && (
					<motion.p
						initial={{ opacity: 0, y: -10 }}
						animate={{ opacity: 1, y: 0 }}
						className="text-sm text-red-500"
					>
						{errors.confirmPassword}
					</motion.p>
				)}
			</div>
		</motion.div>
	);
};

export default AccountStep;
