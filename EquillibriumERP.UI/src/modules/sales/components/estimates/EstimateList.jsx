import EstimateTable from "./EstimateTable";

export default function EstimateList({ estimates, onView }) {

    return (

        <EstimateTable
            estimates={estimates}
            onView={onView}
        />

    );

}