import { TextInput } from "flowbite-react";
import React from "react";
import { UseControllerProps, useController } from "react-hook-form";
import "react-datepicker/dist/react-datepicker.css";
import DatePicker, { ReactDatePickerProps } from "react-datepicker";
type Props = {
  type?: string;
  placeholder?: string;
} & UseControllerProps &
  Partial<ReactDatePickerProps>;
export default function DateInput(props: Props) {
  const { field, fieldState } = useController({ ...props, defaultValue: "" });
  return (
    <div className="mb-3 block">
      <DatePicker
        {...props}
        {...field}
        onChange={(value) => field.onChange(value)}
        selected={field.value}
        placeholderText={props.placeholder ?? ""}
        className={`rounded-lg w-[100%] ${
          fieldState.error ? "border-red-600" : "border-gray-300"
        }`}
      />
      {fieldState.error && (
        <div className="text-red-600">{fieldState.error.message}</div>
      )}
    </div>
  );
}
