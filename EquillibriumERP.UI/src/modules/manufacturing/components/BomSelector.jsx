import { useEffect, useState } from "react";

import { getAllBoms } from "../services/bomService";

export default function BomSelector({
    value,
    onChange,
    disabled = false
}) {

    const [boms, setBoms] = useState([]);

    useEffect(() => {

        loadBoms();

    }, []);

    async function loadBoms() {

        try {

            const data = await getAllBoms();

            setBoms(data);

        }
        catch (error) {

            console.error(error);

            setBoms([]);

        }

    }

    return (

        <select
            value={value ?? ""}
            disabled={disabled}
            onChange={(e) => onChange(e.target.value)}
        >

            <option value="">
                -- Select BOM --
            </option>

            {

                boms.map(bom => (

                    <option
                        key={bom.id}
                        value={bom.id}
                    >
                        {bom.code} - {bom.name}
                    </option>

                ))

            }

        </select>

    );

}