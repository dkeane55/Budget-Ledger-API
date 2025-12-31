Budget Ledger API
This project began as a simple CRUD budgeting API. While functional, that approach broke down once corrections, historical accuracy, and explainability of transactions were considered.
The core model was refactored to better align with how financial systems behave in practice.

Reason for refactor
Originally transactions could be updated or deleted directly. This had several issues:
- Historical data could be silently overwritten
- Category totals could change without explanation
- Corrections erased the original intent of the transaction
For transaction data this undermines traceability.

Current Design
The system now uses an append only journal entry model.
Instead of modifying or deleting transactions, the application records:
- Original entries to represent new expenses
- Reversal entries to negate previous entries when a correction is made
Each entry is immutable and stored permanently. Corrections (such as reclassifying an expense) are modeled by appending new entries instead of changing existing data.

How Totals Are Calculated
Category summaries are calculated by using:
- Sum of all original entries
- Minus the sum of all reversal entries

Scope
This is intentionally not a full accounting system.
The focus is on demonstrating correctness, traceability, and intent-driven design without introducing unnecessary complexity.
