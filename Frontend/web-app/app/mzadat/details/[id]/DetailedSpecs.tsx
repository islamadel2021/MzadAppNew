"use client";
import { Mzad } from "@/types";
import { Table } from "flowbite-react";

type Props = {
  mzad: Mzad;
};
export default function DetailedSpecs({ mzad }: Props) {
  return (
    <Table>
      <Table.Body className="divide-y  text-gray-600  text-lg ">
        <Table.Row>
          <Table.Cell className="text-gray-900">Seller</Table.Cell>
          <Table.Cell>{mzad.seller}</Table.Cell>
        </Table.Row>
        <Table.Row>
          <Table.Cell className="whitespace-nowrap  text-gray-900 ">
            Father
          </Table.Cell>
          <Table.Cell>{mzad.father}</Table.Cell>
        </Table.Row>
        <Table.Row>
          <Table.Cell className="whitespace-nowrap text-gray-900">
            Mother
          </Table.Cell>
          <Table.Cell>{mzad.mother}</Table.Cell>
        </Table.Row>
        <Table.Row>
          <Table.Cell className="whitespace-nowrap text-gray-900 dark:text-white">
            Year of birth
          </Table.Cell>
          <Table.Cell>{mzad.yearOfBirth}</Table.Cell>
        </Table.Row>
        <Table.Row>
          <Table.Cell className="whitespace-nowrap text-gray-900 dark:text-white">
            Breed
          </Table.Cell>
          <Table.Cell>{mzad.breed}</Table.Cell>
        </Table.Row>
        <Table.Row>
          <Table.Cell className="whitespace-nowrap text-gray-900 dark:text-white">
            Color
          </Table.Cell>
          <Table.Cell>{mzad.color}</Table.Cell>
        </Table.Row>
        <Table.Row>
          <Table.Cell className="whitespace-nowrap text-gray-900 dark:text-white">
            Has reserve price?
          </Table.Cell>
          <Table.Cell>{mzad.reservePrice > 0 ? "Yes" : "No"}</Table.Cell>
        </Table.Row>
      </Table.Body>
    </Table>
  );
}
