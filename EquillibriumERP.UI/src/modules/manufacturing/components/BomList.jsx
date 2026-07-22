import BomTable from "./BomTable";

export default function BomList({ boms, onView }) {

    return (

        <BomTable
            boms={boms}
            onView={onView}
        />
      

    );

}