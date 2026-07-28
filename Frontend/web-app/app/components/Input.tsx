import { TextInput } from "flowbite-react";
import React from "react";
import { UseControllerProps, useController } from "react-hook-form";

type Props = {
  type?: string;
  placeholder?: string;
} & UseControllerProps;
export default function Input(props: Props) {
  const { field, fieldState } = useController({ ...props, defaultValue: "" });
  return (
    <div className="mb-3 block">
      <TextInput
        {...props}
        {...field}
        type={props.type ?? "text"}
        placeholder={props.placeholder ?? ""}
        color={fieldState.error ? "failure" : "gray"}
        helperText={fieldState.error?.message}
      />
    </div>
  );
}
