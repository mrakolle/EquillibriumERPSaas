import "./EstimateTotals.css";

export default function EstimateTotals({ items = [] }) {

    const subtotal = items.reduce(

        (sum, item) =>

            sum +

            (Number(item.quantity) * Number(item.unitPrice)),

        0

    );

    const discountAmount = items.reduce(

        (sum, item) => {

            const lineSubtotal =

                Number(item.quantity) *
                Number(item.unitPrice);

            return sum +

                (lineSubtotal *
                (Number(item.discountPercent) / 100));

        },

        0

    );

    const taxableAmount = subtotal - discountAmount;

    const taxAmount = items.reduce(

        (sum, item) => {

            const lineSubtotal =

                Number(item.quantity) *
                Number(item.unitPrice);

            const discount =

                lineSubtotal *
                (Number(item.discountPercent) / 100);

            const taxable = lineSubtotal - discount;

            return sum +

                (taxable *
                (Number(item.taxRate) / 100));

        },

        0

    );

    const total = taxableAmount + taxAmount;

    return (

        <div className="estimate-totals">

            <table>

                <tbody>

                    <tr>

                        <td>Subtotal</td>

                        <td>
                            R {subtotal.toFixed(2)}
                        </td>

                    </tr>

                    <tr>

                        <td>Discount</td>

                        <td>
                            R {discountAmount.toFixed(2)}
                        </td>

                    </tr>

                    <tr>

                        <td>Vat</td>

                        <td>
                            R {taxAmount.toFixed(2)}
                        </td>

                    </tr>

                    <tr className="estimate-grand-total">

                        <td>Total</td>

                        <td>
                            R {total.toFixed(2)}
                        </td>

                    </tr>

                </tbody>

            </table>

        </div>

    );

}