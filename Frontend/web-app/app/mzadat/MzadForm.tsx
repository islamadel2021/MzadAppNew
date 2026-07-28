"use client";
import React, { useEffect } from "react";
import { FieldValues, useForm } from "react-hook-form";
import { Button, TextInput } from "flowbite-react";
import Input from "../components/Input";
import DateInput from "../components/DateInput";
import { createMzad, updateMzad } from "../actions/MzadActions";
import { usePathname, useRouter } from "next/navigation";
import toast from "react-hot-toast";
import { Mzad } from "@/types";
type Props = { mzad?: Mzad };
export default function MzadForm({ mzad }: Props) {
  const router = useRouter();
  const pathname = usePathname();
  const {
    control,
    handleSubmit,
    reset,
    setFocus,
    formState: { isSubmitting, isValid, isDirty, errors }
  } = useForm({
    mode: "onTouched"
  });
  useEffect(() => {
    setFocus("name");
    if (mzad) {
      const {
        name,
        father,
        mother,
        breed,
        yearOfBirth,
        color,
        imageUrl,
        reservePrice,
        mzadEnd
      } = mzad;
      reset({
        name,
        father,
        mother,
        breed,
        yearOfBirth,
        color,
        imageUrl,
        reservePrice,
        mzadEnd: new Date(mzadEnd)
      });
    }
  }, [setFocus]);
  const onSubmit = async (data: FieldValues) => {
    try {
      let id;
      let res;
      if (pathname === "/mzadat/create") {
        res = await createMzad(data);
        id = res.id;
      } else {
        res = await updateMzad(data, mzad?.id!);
        id = mzad?.id;
      }

      if (res.error) throw res.error;
      router.push(`/mzadat/details/${id}`);
    } catch (error: any) {
      toast.error(error.status + " " + error.message);
    }
  };
  return (
    <form className="flex flex-col mt-3" onSubmit={handleSubmit(onSubmit)}>
      <Input
        control={control}
        name="name"
        placeholder="Horse name"
        rules={{ required: "Horse name is required" }}
      />
      <div className="grid grid-cols-2 gap-4">
        <Input
          control={control}
          name="father"
          placeholder="Father name"
          rules={{ required: "Father name is required" }}
        />
        <Input
          control={control}
          name="mother"
          placeholder="Mother"
          rules={{ required: "Mother name is required" }}
        />
      </div>
      <div className="grid grid-cols-2 gap-4">
        <Input
          control={control}
          name="breed"
          placeholder="Breed"
          rules={{ required: "Breed name is required" }}
        />
        <Input
          control={control}
          name="yearOfBirth"
          type="number"
          placeholder="Year of birth"
          rules={{ required: "Year of birth is required" }}
        />
      </div>
      <div className="grid grid-cols-2 gap-4">
        <Input
          control={control}
          name="color"
          placeholder="Color"
          rules={{ required: "Color is required" }}
        />
        <Input
          control={control}
          name="imageUrl"
          placeholder="Image"
          rules={{ required: "Image is required" }}
        />
      </div>
      <div className="grid grid-cols-2 gap-4">
        <Input
          control={control}
          name="reservePrice"
          type="number"
          placeholder="Reserve price"
          rules={{ required: "Reserve price is required" }}
        />
        <DateInput
          control={control}
          name="mzadEnd"
          placeholder="Mzad end date"
          rules={{ required: "Mzad end date is required" }}
          dateFormat={"dd MMMM yyyy h:mm a"}
          showTimeSelect
        />
      </div>

      <div className="flex justify-between">
        <Button outline color="light" className="focus:ring-0">
          Cancel
        </Button>
        <Button
          isProcessing={isSubmitting}
          disabled={!isValid}
          type="submit"
          outline
          color="purple"
          className="focus:ring-0"
        >
          Submit
        </Button>
      </div>
    </form>
  );
}
