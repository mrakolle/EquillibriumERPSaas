import { useMemo } from "react";

export default function UnitOfMeasureSelector({
    value,
    onChange,
    disabled = false
}) {

    const units = useMemo(() => ([
        "kg",
        "g",
        "mg",
        "L",
        "mL",
        "ton",
        "ea",
        "box",
        "bag",
        "drum",
        "pallet"
    ]), []);

    return (

        <select
            value={value ?? ""}
            disabled={disabled}
            onChange={(e) => onChange(e.target.value)}
        >

            <option value="">
                -- Select Unit --
            </option>

            {

                units.map(unit => (

                    <option
                        key={unit}
                        value={unit}
                    >
                        {unit}
                    </option>

                ))

            }

        </select>

    );

}